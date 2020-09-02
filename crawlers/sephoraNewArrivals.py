import json
import certifi
import urllib3


http = urllib3.PoolManager(cert_reqs='CERT_REQUIRED', ca_certs=certifi.where())

def request_json(url):

    headers = {
        "Accept": "*/*", 
        "Accept-Encoding": "gzip, deflate, br", 
        "Accept-Language": "en-US,en;q=0.9",
        "User-Agent": "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36",
        "Referer": "https://www.sephora.com/sale",
    }

    response = http.request(method='GET', url=url, headers=headers)

    formatted_json = json.loads(response.data.decode('utf-8'))
    return formatted_json








def get_new_arrival_ids(start=1, end=11):
    product_ids = []

    #1. Go through all pages in new arrivals, get Product Ids
    for page_num in range(start, end):

        new_arrivals_url = 'https://www.res-x.com/ws/r2/Resonance.aspx?appid=sephora02&tk=649431862487778&ss=906139100526293&sg=1&pg=res20082620381483407331346&bx=true&sc=content1_rr&page=' + str(page_num)

        json_response = request_json(new_arrivals_url)

        items = json_response['items']
        if len(items) == 0:
            # If we're not getting any results just stop
            break

        for item in items:
            product_id = item['id']
            print(product_id)
            product_ids.append(product_id)

    return product_ids


def parse_sku(current_sku):

    # Get SkuId
    sku_id = current_sku['skuId']

    # Get Image
    sku_images = current_sku['skuImages']
    image_url = sku_images['image135']
    # Get URL
    url = "https://www.sephora.com" + current_sku['targetUrl']

    # Try to get color
    color = None
    if current_sku['variationType'] == 'Color' and 'variationValue' in current_sku:
        # Some products (especially sets of multiple products)
        # don't specify the color. In this case we'll just treat them 
        # as if they had no color at all
        color = current_sku['variationValue']

    # TODO: Parse this into a decimal
    list_price = current_sku['listPrice']

    # Check if it was on sale
    sale_price = None
    if 'salePrice' in current_sku:
        # Get the sale price
        sale_price = current_sku['salePrice']

    # Get size if it is available
    size = None
    if 'size' in current_sku:
        size = current_sku['size']

    return sku_id, image_url, url, color, list_price, sale_price, size


def get_product_info(product_id):

    product_info_url = 'https://www.sephora.com/api/catalog/products/' + product_id

    json_response = request_json(product_info_url)

    brand_info = json_response['brand']
    brand_name = brand_info['displayName']
    retailer = "Sephora"
    product_name = json_response['displayName']

    current_sku = json_response['currentSku']
    
    sku_id, image_url, url, color, list_price, sale_price, size = parse_sku(current_sku)
    print("sku_id", sku_id)
    print("image_url", image_url)
    print("url", url)
    print("color", color)
    print("list_price", list_price)
    print("sale_price", sale_price)
    print("size", size)

    if 'regularChildSkus' in json_response:
        childSkus  = json_response['regularChildSkus']
        print("Child Skus:")

        for child in childSkus:
            sku_id, image_url, url, color, list_price, sale_price, size = parse_sku(child)
            print("sku_id", sku_id)
            print("image_url", image_url)
            print("url", url)
            print("color", color)
            print("list_price", list_price)
            print("sale_price", sale_price)
            print("size", size)


    if 'onSaleChildSkus' in json_response:
        childSkus  = json_response['onSaleChildSkus']
        print("Child Skus:")

        for child in childSkus:
            sku_id, image_url, url, color, list_price, sale_price, size = parse_sku(child)
            print("sku_id", sku_id)
            print("image_url", image_url)
            print("url", url)
            print("color", color)
            print("list_price", list_price)
            print("sale_price", sale_price)
            print("size", size)



product_ids = get_new_arrival_ids(start=1, end=2)

for id in product_ids:
    get_product_info(id)

    
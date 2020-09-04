import pyodbc
import os

conn_string = os.getenv("MAKEUP_PYODBC") 

def insert_many(query, args):
    """
    Adapted from: https://stackoverflow.com/questions/8134602/psycopg2-insert-multiple-rows-with-one-query
    """

    if len(args) == 0:
        return

    connection = pyodbc.connect(conn_string)
    cursor = connection.cursor()

    numArgs = len(args[0])
    #NOTE: We are not string concatenating user input here as that would be vulernable to SQL injection
    placeholder = '(' + ','.join(['%s'] * numArgs) + ')'

    #We use mogrify to embed input and avoid SQL injection. 
    args_str = ','.join(cursor.mogrify(placeholder, x).decode('utf-8') for x in args)

    cursor.execute(query + args_str)
    connection.commit()

    cursor.close()
    connection.close()

def insert_query(query, args=None):
    """(str) -> list of tuple
    Return the result of executing the query
    (name/path to a database)"""
    connection = pyodbc.connect(conn_string)
    cursor = connection.cursor()

    if args is None:
        cursor.execute(query)
    else:
        cursor.execute(query, args)

    connection.commit()

    cursor.close()
    connection.close()

def update_query(query, args = None):
    """(str) -> None
    """
    connection = pyodbc.connect(conn_string)
    cursor = connection.cursor()

    if args is None:
        cursor.execute(query)
    else:
        cursor.execute(query, args)

    cursor.close()
    connection.close()

    
def select_query(query, args=None):
    """(str) -> list of tuple
    Return the result of executing the query
    (name/path to a database)"""
    connection = pyodbc.connect(conn_string)
    cursor = connection.cursor()

    if args is None:
        cursor.execute(query)
    else:
        cursor.execute(query, args)

    results = cursor.fetchall()

    cursor.close()
    connection.close()

    return results


def select_one(query, args=None):
    """(str) -> list of tuple
    Return the result of executing the query
    (name/path to a database)"""
    connection = pyodbc.connect(conn_string)
    cursor = connection.cursor()

    if args is None:
        cursor.execute(query)
    else:
        cursor.execute(query, args)

    results = cursor.fetchone()

    cursor.close()
    connection.close()

    return results

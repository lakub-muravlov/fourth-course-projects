import time
import socket
import pickle
import sys

HOST = "127.0.0.1"
PORT = 8080

print("Starting server")

SOCKET = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
SOCKET.bind((HOST, PORT))
SOCKET.listen()
CONNECTION, ADDRESS = SOCKET.accept()

print(f"Connected {ADDRESS} to {SOCKET}")
TIME = time.time()
while True:
    DATA = CONNECTION.recv(1024)
    if not DATA: 
        break

    DATA_DECODED = pickle.loads(DATA)
    print(f"Got data\n\tData size:  {sys.getsizeof(DATA_DECODED)}\n\tData:  {DATA_DECODED}\n\tData type:  {type(DATA_DECODED)}")
    print(f"{time.time() - TIME}s since server started")

time.sleep(10)
CONNECTION.close()
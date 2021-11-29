import time
import socket
import pickle
import sys

HOST = "127.0.0.1"
PORT = 4200


SOCKET = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
SOCKET.bind((HOST, PORT))
print("UDP server started and listens")

TIME = time.time()
while True:
    DATA, ADDRESS = SOCKET.recvfrom(1024)
    DATA_DECODED = pickle.loads(DATA)
    print(f"Got data from {ADDRESS}.\n\tData size:  {sys.getsizeof(DATA_DECODED)}\n\tData:  {DATA_DECODED}\n\tData type:  {type(DATA_DECODED)}")
    print(f"{time.time() - TIME}s since server started")
    if not DATA:
        break
    

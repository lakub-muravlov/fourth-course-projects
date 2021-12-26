import numpy as np
from PIL import Image

#options interfaces
class IImgOpts:
    def __init__(self, size, whiteBlack):
        self.size = size
        self.whiteBlack = whiteBlack

class INetworkOpts:
    def __init__(self, theta, iterations):
        self.theta = theta
        self.iterations = iterations

#image processing functions
def imgToVec(file, imgOpts: IImgOpts):
    img = Image.open(file).convert("L").resize(imgOpts.size)
    imgBits = np.asarray(img, dtype=np.unit8)
    imgVec = np.zeros(imgOpts.size, dtype=float)
    imgVec[imgBits > imgOpts.whiteBlack] = 1
    imgVec[imgBits == 0] = -1
    return imgVec

def vecToImg(imgVec):
    imgBits = np.zeros(imgVec.shape, dtype=np.unit8)
    imgBits[imgVec == 1] = 255
    imgBits[imgVec == -1] = 0
    img = Image.fromarray(imgBits, mode='L')
    return img

def matrixToVec(matrix):
    result = [matrix[i,j] for i in range(matrix.shape[0]) for j in range(matrix.shape[1])]
    return np.array(result)
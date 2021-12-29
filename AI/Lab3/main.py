from PIL import Image
import random
import numpy as np
import matplotlib.pyplot as plt

#options interfaces
class IImgOpts:
    def __init__(self, imgSize, blackLimit):
        self.imgSize = imgSize
        self.blackLimit = blackLimit

class INetworkOpts:
    def __init__(self, theta, iters):
        self.theta = theta
        self.iters = iters

#image processing functions
#process image from byte representation to [1,-1] matrix
def imgToHopfieldMatrix(image, opts: IImgOpts):
    img = Image.fromImage(image).convert("L").resize(opts.imgSize)
    imgArr = np.asarray(img, dtype=np.unit8)
    hopfieldMatrix = np.zeros(opts.imgSize, dtype=float)
    hopfieldMatrix[imgArr > opts.blackLimit] = 1
    hopfieldMatrix[imgArr == 0] = -1
    return hopfieldMatrix

#process matrix back to image
def hopfieldMatrixToImg(imgArr):
    convertArray = np.zeros(imgArr.shape, dtype=np.unit8)
    convertArray[imgArr == 1] = 255
    convertArray[imgArr == -1] = 0
    img = Image.fromarray(convertArray, mode = "L")
    return img

#transform matrix to vector for network
def matrixToVec(matrix):
    result = [matrix[i,j] for i in range(matrix.shape[0]) for j in range(matrix.shape[1])]
    return np.array(result)

#hopfield network code
def setWeights(etalonImage):
    weights = np.zeros([len(etalonImage), len(etalonImage)])
    for i in range(len(etalonImage)):
        for j in range(i, len(etalonImage)):
            weights[i, j] = 0 if i == j else etalonImage[i] * etalonImage[j]
            weights[j, i] = weights[i, j]
    return weights

#local minimums of functional
def getEnergy(weights, input, bias=0):
    energy = -input.dot(weights).dot(input.T) + sum(bias * input)
    return energy

#updating weights of image that is being recognized
def updateWeights(weights, vector, theta=1, iters=2000):
    energies = list()
    iterations = list()
    energies.append(getEnergy(weights, vector))
    iterations.append(0)
    for i in range(iters):
        length = len(vector)
        updateIndex = random.randint(0, length - 1)
        nx = np.dot(weights[updateIndex][:], vector) - theta
        vector[updateIndex] = 1 if nx >= 0 else -1
        iterations.append(i)
        energies.append(getEnergy(weights, vector))
    return vector, iterations, energies

def trainNetwork(img, opts):
    weights = None
    for i,path in enumerate(img):
        matrix = imgToHopfieldMatrix(path, opts)
        vector = matrixToVec(matrix)
        plt.imshow(hopfieldMatrixToImg(matrix))
        plt.title("Зображення на якому проводилось тренування")
        plt.show()
        if i == 0:
            weights = setWeights(vector)
        else:
            weights = weights + setWeights(vector)
    weights = weights / len(img)
    return weights

def imgRecognition(weights, img, imgOpts, networkOpts):
    matrix = imgToHopfieldMatrix(img, imgOpts)
    vector = matrixToVec(matrix)
    imgBeforeRecognition = hopfieldMatrixToImg(matrix)
    #оновлення вагів для зображення що розпізнається
    recVector, iters, energies = updateWeights(weights, vector, networkOpts.theta, networkOpts.iters)
    #виведення зображення до розпізнавання
    plt.subplot(221)
    plt.imshow(imgBeforeRecognition)
    plt.title("Зображення, що розпізнається")
    #виведення зображення після розпізнавання
    plt.subplot(222)
    plt.imshow(hopfieldMatrixToImg(recVector.reshape(matrix.shape)))
    plt.title('Розпізнане зображення')
    #статистика навчання мережі
    plt.subplot(212)
    plt.plot(iters, energies)
    plt.ylabel("Локальні мінімуми")
    plt.xlabel("Ітерація")
    plt.show()

def hopfield(pathToTrainImg, pathToTestImg, imgOpts, networkOpts):
    weightsOfTrained = trainNetwork([pathToTrainImg], imgOpts)
    imgRecognition(weightsOfTrained, pathToTestImg, imgOpts, networkOpts)

def main():
    imgOpts = IImgOpts((100,100), 145)
    networkOpts = INetworkOpts(0.75, 15000)
    weightsOfTrained = trainNetwork(['train/ji.png'], imgOpts)
    imgRecognition(weightsOfTrained, 'test/ji.png', imgOpts, networkOpts)
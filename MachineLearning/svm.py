import numpy as np
import matplotlib.pyplot as plt
from sklearn import svm
from helpPlot import *

data_train = np.loadtxt('wine.csv', delimiter=';')

X=data_train[:,8:10]
Y=data_train[:,11]

i=0
while(i<len(Y)):
    if(Y[i]<5):
        Y[i] = 0
    elif(Y[i]>6):
        Y[i]=1
    else:
        X = np.delete(X, i, 0)
        Y = np.delete(Y, i)
        i-=1
    i += 1

plt.figure(1)
plt.scatter(X[:, 0], X[:,1], c=Y, edgecolors='k', cmap=plt.cm.Paired)

Xte = X[0:20,:]
Yte = Y[0:20]

Xtr = X[20:,:]
Ytr = Y[20:]

# Kernels can be linear, rbf, poly
model = svm.SVC(C=1000, kernel='linear')
model.fit(Xtr,Ytr)

plt.figure(2)

xx, yy = make_mesh(X,Y)
plot_decision_boundary_contour(model, xx,yy)

plt.scatter(Xtr[:, 0], Xtr[:, 1], c=Ytr, edgecolors='k', cmap=plt.cm.Paired)
plt.title("linear kernel")

predictions = model.predict(Xte)
total = 0
i=0
while i<len(predictions):
    if(predictions[i] != Yte[i]):
        total +=1
    i+=1
print("Total errors were " + str(total))

plt.show()

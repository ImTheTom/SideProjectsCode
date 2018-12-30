import numpy as np
import matplotlib.pyplot as plt
from dataCreation import *
from sklearn import linear_model
from helpPlot import *


data = DataCreate('iris.csv', [0,1,2,3], [4])
data = data.get_data()
data = np.array(data)
Xtr = data[:,:2]
Ytr = data[:,4:]

XtrA = []
YtrA = []

i=0
while(i<Xtr.shape[0]):
    if(Ytr[i]!=2):
        XtrA.append(Xtr[i])
        YtrA.append(Ytr[i])
    i += 1

XtrA=np.array(XtrA)
YtrA=np.array(YtrA)

plt.figure(1)
plt.scatter(XtrA[:, 0], XtrA[:, 1], c=YtrA.ravel(), edgecolors='k', cmap=plt.cm.Paired)
plt.show()

modelA = linear_model.LogisticRegression()

modelA.fit(XtrA, YtrA.ravel())

plt.figure(2)

xx, yy = make_mesh(XtrA,YtrA)
plot_decision_boundary_contour(modelA, xx,yy)

plt.scatter(XtrA[:, 0], XtrA[:, 1], c=YtrA.ravel(), edgecolors='k', cmap=plt.cm.Paired)
plt.xlabel('Sepal length')
plt.ylabel('Sepal width')
plt.show()

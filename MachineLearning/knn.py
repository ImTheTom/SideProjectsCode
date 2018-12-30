import numpy as np
import matplotlib.pyplot as plt
from dataCreation import *
from helpPlot import *
from sklearn.neighbors import KNeighborsClassifier

data = DataCreate('iris.csv', [0,1,2,3], [4])
data = data.get_data()
data = np.array(data)
X = data[:,:2]
Y = data[:,4:]

Xtr = []
Ytr = []
Xte = []
Yte = []

i=0
testValue=False
while(i<len(Y)):
    if(testValue):
        Xte.append(X[i])
        Yte.append(Y[i])
        testValue=False
    else:
        Xtr.append(X[i])
        Ytr.append(Y[i])
        testValue=True
    i += 1

Xtr=np.array(Xtr)
Ytr=np.array(Ytr)
Xte=np.array(Xte)
Yte=np.array(Yte)

model = KNeighborsClassifier(n_neighbors=2)
model.fit(Xtr,Ytr.ravel())

xx, yy = make_mesh(Xtr,Ytr)
plot_decision_boundary_contour(model, xx,yy)

plt.scatter(Xtr[:, 0], Xtr[:, 1], c=Ytr.ravel(), edgecolors='k', cmap=plt.cm.Paired)
plt.title('K value of 2')
plt.xlabel('Sepal length')
plt.ylabel('Sepal width')

predictions = model.predict(Xte)
total = 0
i=0
while i<len(predictions):
    if(predictions[i] != Yte[i]):
        total +=1
    i+=1
print("Total errors were " + str(total)+" out of "+str(len(Xte)))

plt.show()

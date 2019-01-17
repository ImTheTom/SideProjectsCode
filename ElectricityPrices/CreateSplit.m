function [ xTrain, xTest, yTrain, yTest ] = CreateSplit( x,y,split )
%CREATESPLIT Used to split the data randomly
iTest = randperm(length(x),length(x)*split);
iTrain = setdiff(1:length(x), iTest);
xTest = x(iTest,:); yTest = y(iTest);
xTrain = x(iTrain,:); yTrain = y(iTrain);
end


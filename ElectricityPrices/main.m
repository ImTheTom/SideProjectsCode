%% Initialise
clear all;
close all;
clc;
rng default;

%% Load in Data
[qDate,qDemand,qPrice] = GetQueenslandData();

[saDate,saDemand,saPrice] = GetSouthAustraliaData();

[qPreviousDate,qPreviousDemand,qPreviousPrice] = OldQueenslandData();

[saPreviousDate,saPreviousDemand,saPreviousPrice] = OldSouthAustraliaData();

%% Plot Data
PlotData(qDate,qDemand,qPrice,'Queensland');

PlotData(saDate,saDemand,saPrice,'South Australia');

%% Get date arrays for models
dateArray = [qDate.Month,qDate.Day,qDate.Hour,qDate.Minute];

dateArrayPrevious = [qPreviousDate.Month,qPreviousDate.Day,qPreviousDate.Hour,qPreviousDate.Minute];

%% Get indexes for winter and summer
startSummerIndex = FindIndex('01-Dec-2018 00:00:00', qDate);
endSummerIndex = FindIndex('28-Feb-2018 23:30:00', qDate);

startWinterIndex = FindIndex('01-Jun-2018 00:00:00', qDate);
endWinterIndex = FindIndex('31-Aug-2018 23:30:00', qDate);

%% Get mean values
fprintf('Mean values for QLD in Summer and Winter\n');

GetMeanSummer(qDemand, startSummerIndex, endSummerIndex, 'Queensland', 'Demand');
GetMeanSummer(qPrice, startSummerIndex, endSummerIndex, 'Queensland', 'Price');

GetMeanWinter(qDemand, startWinterIndex, endWinterIndex, 'Queensland', 'Demand');
GetMeanWinter(qPrice, startWinterIndex, endWinterIndex, 'Queensland', 'Price');

fprintf('\nMean values for SA in Summer and Winter\n');

GetMeanSummer(saDemand, startSummerIndex, endSummerIndex, 'South Australia', 'Demand');
GetMeanSummer(saPrice, startSummerIndex, endSummerIndex, 'South Australia', 'Price');

GetMeanWinter(saDemand, startWinterIndex, endWinterIndex, 'South Australia', 'Demand');
GetMeanWinter(saPrice, startWinterIndex, endWinterIndex, 'South Australia', 'Price');

%% Calculate training error in models
fprintf('\nCalulate error for training against all data points\n');

modelQLDDemand = fitlm(dateArray,qDemand);
modelQLDPrice = fitlm(dateArray,qPrice);

modelSADemand = fitlm(dateArray,saDemand);
modelSAPrice = fitlm(dateArray,saPrice);

CalculateError(qDemand, dateArray, modelQLDDemand, 'Queensland', 'Training', 'Demand');
CalculateError(qPrice, dateArray, modelQLDPrice, 'Queensland', 'Training', 'Price');

CalculateError(saDemand, dateArray, modelSADemand, 'South Australia', 'Training', 'Demand');
CalculateError(saPrice, dateArray, modelSAPrice, 'South Australia', 'Training', 'Price');

%% Calculate testing error in models
fprintf('\nRandom data points to train and test for missing points in QLD\n');

[xTrain, xTest, yTrain, yTest] = CreateSplit( dateArray, qDemand, 0.5 );
model = fitlm(xTrain,yTrain);
CalculateError( yTest, xTest, model, 'Queensland', 'Testing', 'Demand' );

[xTrain, xTest, yTrain, yTest] = CreateSplit( dateArray, qPrice, 0.5 );
model = fitlm(xTrain,yTrain);
CalculateError( yTest, xTest, model, 'Queensland', 'Testing', 'Price' );

fprintf('\nRandom data points to train and test for missing points in South Australia\n');

[xTrain, xTest, yTrain, yTest] = CreateSplit( dateArray, saDemand, 0.5 );
model = fitlm(xTrain,yTrain);
CalculateError( yTest, xTest, model, 'South Australia', 'Testing', 'Demand' );

[xTrain, xTest, yTrain, yTest] = CreateSplit( dateArray, saPrice, 0.5 );
model = fitlm(xTrain,yTrain);
CalculateError( yTest, xTest, model, 'South Australia', 'Testing', 'Price' );

%% Test how well a model can predict a value under a threshold across various models in QLD
fprintf('\nRandom data points to train and test for missing points for threshold in QLD\n');

[xTrain, xTest, yTrain, yTest] = CreateSplit( dateArray, qPrice, 0.5 );
model = fitlm(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'Queensland', 'linear');

model = fitrgp(xTrain,yTrain, 'KernelFunction','ardsquaredexponential' );
CalculateThreshold( xTest, yTest, 75, model, 'Queensland', 'Gaussian process regression');

model = fitrsvm(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'Queensland', 'SVM Linear');

model = fitrsvm(xTrain,yTrain, 'KernelFunction', 'gaussian');
CalculateThreshold( xTest, yTest, 75, model, 'Queensland', 'SVM Gaussian');

model = fitrlinear(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'Queensland', 'High deminsonal Linear Regression');

model = fitrtree(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'Queensland', 'Tree Regression');

model = fitrensemble(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'Queensland', 'Ensemble of learners for regression');

%% Test how well a model can predict a value under a threshold across various models in South Australia
fprintf('\nRandom data points to train and test for missing points for threshold in South Australia\n');

[xTrain, xTest, yTrain, yTest] = CreateSplit( dateArray, saPrice, 0.5 );
model = fitlm(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 90, model, 'South Australia', 'Generalized linear regression' );

model = fitrgp(xTrain,yTrain, 'KernelFunction','ardsquaredexponential' );
CalculateThreshold( xTest, yTest, 75, model, 'South Australia', 'Gaussian process regression');

model = fitrsvm(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'South Australia', 'SVM Linear');

model = fitrsvm(xTrain,yTrain, 'KernelFunction', 'gaussian');
CalculateThreshold( xTest, yTest, 75, model, 'South Australia', 'SVM Gaussian');

model = fitrlinear(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'South Australia', 'High deminsonal Linear Regression');

model = fitrtree(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'South Australia', 'Tree Regression');

model = fitrensemble(xTrain,yTrain);
CalculateThreshold( xTest, yTest, 75, model, 'South Australia', 'Ensemble of learners for regression');

%% Test how well a model can predict a value under a threshold across various models using previous year data in QLD
fprintf('\nPreivous years data to model 2018 QLD\n');

model = fitlm(dateArrayPrevious,qPreviousPrice);
CalculateThreshold( dateArray, qPrice, 75, model, 'Queensland', 'linear');

model = fitrgp(dateArrayPrevious,qPreviousPrice, 'KernelFunction','ardsquaredexponential' );
CalculateThreshold( dateArray, qPrice, 75, model, 'Queensland', 'Gaussian process regression');

model = fitrsvm(dateArrayPrevious,qPreviousPrice);
CalculateThreshold( dateArray, qPrice, 75, model, 'Queensland', 'SVM Linear');

model = fitrsvm(dateArrayPrevious,qPreviousPrice, 'KernelFunction', 'gaussian');
CalculateThreshold( dateArray, qPrice, 75, model, 'Queensland', 'SVM Gaussian');

model = fitrlinear(dateArrayPrevious,qPreviousPrice);
CalculateThreshold( dateArray, qPrice, 75, model, 'Queensland', 'High deminsonal Linear Regression');

model = fitrtree(dateArrayPrevious,qPreviousPrice);
CalculateThreshold( dateArray, qPrice, 75, model, 'Queensland', 'Tree Regression');

model = fitrensemble(dateArrayPrevious,qPreviousPrice);
CalculateThreshold( dateArray, qPrice, 75, model, 'Queensland', 'Ensemble of learners for regression');

%% Test how well a model can predict a value under a threshold across various models using previous year data in QLD
fprintf('\nPreivous years data to model 2018 SA\n');

model = fitlm(dateArrayPrevious,saPreviousPrice);
CalculateThreshold( dateArray, saPrice, 75, model, 'South Australia', 'linear');

model = fitrgp(dateArrayPrevious,saPreviousPrice, 'KernelFunction','ardsquaredexponential' );
CalculateThreshold( dateArray, saPrice, 75, model, 'South Australia', 'Gaussian process regression');

model = fitrsvm(dateArrayPrevious,saPreviousPrice);
CalculateThreshold( dateArray, saPrice, 75, model, 'South Australia', 'SVM Linear');

model = fitrsvm(dateArrayPrevious,saPreviousPrice, 'KernelFunction', 'gaussian');
CalculateThreshold( dateArray, saPrice, 75, model, 'South Australia', 'SVM Gaussian');

model = fitrlinear(dateArrayPrevious,saPreviousPrice);
CalculateThreshold( dateArray, saPrice, 75, model, 'South Australia', 'High deminsonal Linear Regression');

model = fitrtree(dateArrayPrevious,saPreviousPrice);
CalculateThreshold( dateArray, saPrice, 75, model, 'South Australia', 'Tree Regression');

model = fitrensemble(dateArrayPrevious,saPreviousPrice);
CalculateThreshold( dateArray, saPrice, 75, model, 'South Australia', 'Ensemble of learners for regression');
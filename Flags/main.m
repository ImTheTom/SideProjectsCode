clear all;
close all;
clc;
%% Load in data
data = readtable('flagData.csv');
%%
data = SetCategoricalValues(data, 18);
data = SetCategoricalValues(data, 29);
data = SetCategoricalValues(data, 30);
%% Set up predictors and response variables
data =  table2cell(data(:, 2:end));
data = cell2mat(data);
%% Split dataset into training and testing
[training, testing] = SplitDataset(data, 0.8, 1);
%% Set up the categorical columns for SVM
categoricalColumns = [3,4,5,6,7,8,9,10,17,18,19,20,21];
%% Set up responses column for 1 vs All models to allow for multiclass classification for svm and linear models
numberOfResponses = length(unique(training(:,1)));
responses = zeros(155, numberOfResponses);
for i=1:numberOfResponses
    responses(:,i) = training(:,1) == i;
end
%% Train and test tree model
tree = fitctree(training(:, 8:end),training(:,1));
[treeTrainingAccuracy, treeTrainingNumWrong] = TestAccuracy(tree, training);
[treeTestingAccuracy, treeTestingNumWrong] = TestAccuracy(tree, testing);
%% Train and test KNN model while finding best K value
kValues = 1:20;
bestKnnTestingAccuracy = 0;
bestKnnTrainingAccuracy = 0;
bestKValue = 0;
for i=1:length(kValues)
    knn = fitcknn(training(:, 8:end),training(:,1), 'NumNeighbors', kValues(i));
    [knnTrainingAccuracy, knnTrainingNumWrong] = TestAccuracy(knn, training);
    [knnTestingAccuracy, knnTestingNumWrong] = TestAccuracy(knn, testing);
    if(knnTestingAccuracy>bestKnnTestingAccuracy)
        bestKnnTestingAccuracy = knnTestingAccuracy;
        bestKnnTrainingAccuracy = knnTrainingAccuracy;
        bestKValue = kValues(i);
    end
end
%% Train and test Disciminant analysis model
discrimtAnal = fitcdiscr(training(:, 8:end),training(:,1));
[discrimtAnalTrainingAccuracy, discrimtAnalTrainingNumWrong] = TestAccuracy(discrimtAnal, training);
[discrimtAnalTestingAccuracy, discrimtAnalTestingNumWrong] = TestAccuracy(discrimtAnal, testing);
%% Train and test SVM models
for i=1:numberOfResponses
    svmModels{i} = fitcsvm(training(:, 8:end), responses(:,i),'CategoricalPredictors', categoricalColumns);
end
[svmTrainingAccuracy, svmTrainingNumWrong] = TestAccuracyForMultipleModels(svmModels, training);
[svmTestingAccuracy, svmTestingNumWrong] = TestAccuracyForMultipleModels(svmModels, testing);
%% Train and test Linear models
for i=1:numberOfResponses
    linearModels{i} = fitlm(training(:, 8:end), responses(:,i));
end
[linearTrainingAccuracy, linearTrainingNumWrong] = TestAccuracyForMultipleModels(linearModels, training);
[linearTestingAccuracy, linearTestingNumWrong] = TestAccuracyForMultipleModels(linearModels, testing);
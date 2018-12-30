function [ training, testing ] = SplitDataset( data, split, rngValue )
%SPLITDATASET Splits dataset into training and testing
    rng(rngValue)
    lengthOfData = length(data);
    trainingTestingSplit = round(lengthOfData*split);

    trainingIndexes = randperm(lengthOfData, trainingTestingSplit);
    testingIndexes = setdiff(1:lengthOfData, trainingIndexes);

    training = data(trainingIndexes, :);
    testing = data(testingIndexes, :);
end


function [ accuracy, numWrong ] = TestAccuracyForMultipleModels( svmModels, testing )
%TestAccuracyForMultipleModels Summary of this function goes here
numberOfResponses = length(svmModels);
probability = zeros(1,numberOfResponses);
numWrong = 0;
testingLength = length(testing);

for i=1:testingLength
    actual = testing(i,1);
    for j=1:numberOfResponses
        probability(j) = predict(svmModels{j}, testing(i, 8:end));
    end
    [amount, prediction] = max(probability);
    if(prediction ~= actual || amount == 0)
        numWrong = numWrong + 1;
    end 
end
accuracy = (1-(numWrong/testingLength)) * 100;
end


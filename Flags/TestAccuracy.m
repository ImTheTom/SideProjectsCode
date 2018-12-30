function [ accuracy, numWrong ] = TestAccuracy( model, testing )
%TESTACCURACY Gets the accuracy of the model
numWrong = 0;
testingLength = length(testing);

for i = 1:testingLength
    actual = testing(i,1);
    prediction = predict(model, testing(i, 8:end));
    if(actual ~= prediction)
        numWrong = numWrong + 1;
    end
end

accuracy = (1-(numWrong/testingLength)) * 100;
end


function [ truePositive, trueNegative, falsePositive, falseNegative ] = CalculateThreshold( xTest, yTest, threshold, model, state, modeltype )
%CALCULATEPRICEUNDERTHRESHOLD Used to calculate TP, TN, FP, FN of a
%prediction being being under a threshold
truePositive = 0;
trueNegative = 0;
falsePositive = 0;
falseNegative = 0;
for i=1:length(xTest)
    predicted = model.predict(xTest(i,:));
    actual = yTest(i);
    if predicted < threshold
        if actual < threshold
            truePositive = truePositive + 1;
        else
            falsePositive = falsePositive + 1;
        end
    else
        if actual > threshold
            trueNegative = trueNegative + 1;
        else
            falseNegative = falseNegative + 1;
        end
    end
end
fprintf('%s %s TP: %d TN: %d FP: %d FN: %d\n',modeltype, state, truePositive, trueNegative, falsePositive, falseNegative);
end


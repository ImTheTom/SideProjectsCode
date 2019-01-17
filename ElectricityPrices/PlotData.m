function [] = PlotData(date,demand,price,state)
%PLOTDATA Used to Plot the data
figure;
subplot(2,1,1);
plot(date,demand);
title(state);
xlabel('Date')
ylabel('Demand')

subplot(2,1,2); 
plot(date,price);
xlabel('Date')
ylabel('Price')
end
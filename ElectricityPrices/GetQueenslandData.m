function [ qDate, qDemand, qPrice ] = GetQueenslandData()
%GETQUEENSLANDDATA Used to get QLD data
queensland = readtable('Queensland/2018/total.csv');
qDate = queensland.SETTLEMENTDATE;
qDate=datetime( qDate,'InputFormat','yyyy/MM/dd HH:mm:ss');
qDemand = queensland.TOTALDEMAND;
qPrice = queensland.RRP;
end
function [ qPreviousDate, qPreviousDemand, qPreviousPrice ] = OldQueenslandData( )
%OLDQUEENSLANDDATA Used to get OLD QLD data
queensland2017 = readtable('Queensland/2017/total.csv');
qDate2017 = queensland2017.SETTLEMENTDATE;
qDate2017=datetime( qDate2017,'InputFormat','yyyy/MM/dd HH:mm:ss');
qDemand2017 = queensland2017.TOTALDEMAND;
qPrice2017 = queensland2017.RRP;

queensland2016 = readtable('Queensland/2016/total.csv');
qDate2016 = queensland2016.SETTLEMENTDATE;
qDate2016 = datetime( qDate2016,'InputFormat','yyyy/MM/dd HH:mm:ss');
qDemand2016 = queensland2016.TOTALDEMAND;
qPrice2016 = queensland2016.RRP;

qPreviousDate = [qDate2016; qDate2017];
qPreviousPrice = [qPrice2016; qPrice2017];
qPreviousDemand = [qDemand2016; qDemand2017];
end
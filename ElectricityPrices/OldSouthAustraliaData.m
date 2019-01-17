function [ saPreviousDate, saPreviousDemand, saPreviousPrice ] = OldSouthAustraliaData( )
%OLDSOUTHAUSTRALIADATA Used to get OLD SA Data
southAustralia2017 = readtable('SouthAustralia/2017/total.csv');
saDate2017 = southAustralia2017.SETTLEMENTDATE;
saDate2017=datetime( saDate2017,'InputFormat','yyyy/MM/dd HH:mm:ss');
saDemand2017 = southAustralia2017.TOTALDEMAND;
saPrice2017 = southAustralia2017.RRP;

southAustralia2016 = readtable('Queensland/2016/total.csv');
saDate2016 = southAustralia2016.SETTLEMENTDATE;
saDate2016 = datetime( saDate2016,'InputFormat','yyyy/MM/dd HH:mm:ss');
saDemand2016 = southAustralia2016.TOTALDEMAND;
saPrice2016 = southAustralia2016.RRP;

saPreviousDate = [saDate2016; saDate2017];
saPreviousPrice = [saPrice2016; saPrice2017];
saPreviousDemand = [saDemand2016; saDemand2017];
end
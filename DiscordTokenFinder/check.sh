if ! pgrep -f 'TokenFinder.py'
then
    /home/pi/Desktop/Code/TokenFinder/launcher.sh
else
    echo "running"
fi
if ! pgrep -f 'main.py'
then
    /home/pi/Desktop/Code/discordBot/launcher.sh
else
    echo "running"
fi
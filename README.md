# PingWall

The PingWall App is a simple utility for monitoring the availabilty of a network connection. It is meant to be used in
monitoring screens at Network Operation Centers and the like. Of course, you may give it any use you want.
It uses the ICMP protocol, like the command line utility *Ping*, available in just about every operating system.



![PingWall blank screen](https://www.irazu.com.ar/pingwall/readme-assets/1Blank.png "PingWall blank Screen")

It is very easy to use. To start, click on the [+] button. 

![First item editing](https://www.irazu.com.ar/pingwall/readme-assets/2EditMode.png "First item editing")

A new 'card' will be created, ready for the information to be entered. Just enter the Hostname (it can be a valid URL or IP address) and the Refresh Period. The latter is the time, in milliseconds, between ICMP requests ("pings"). A default value of 1500 is provided. Avoid going too low if you don't want to overwhelm the connection (the minimum allowed is 200 ms). You can also specify a Display Name to identify this connection with a user-friendly name.
Note: ICMP requests are always sent with 32 bytes of data.

When you are ready, just press *Start*.

![First item running](https://www.irazu.com.ar/pingwall/readme-assets/2Running.png "First item running")

The card will change, and show the latest response time, in miliseconds. You can also see the Display Name. On the left, there is a vertical 'health bar' that shows the percentage of Good vs Bad responses in the last hour (all green = all good). Also, you will see every time a ping response arrives, the number of milliseconds will turn momentarily green, this way you can know it is being regularly updated.

![An error occurred](https://www.irazu.com.ar/pingwall/readme-assets/3Error.png "An error occurred")

If an error occurrs, a message will appear instead of the response time.

You can add as many 'cards' as you like to fill your screen (or more if you don't mind scrolling!). When you close the app and open it again, it will remember the cards you created before and start running immediately).

To edit or delete a card that is already running, simply click the *wrench* icon. The card will stop running and go back to edit mode.

The *Garbage* icon will DELETE the card.
![The Card Action Icons](https://www.irazu.com.ar/pingwall/readme-assets/4Icons.png "The Card Action Icons")

Hope you find this simple utility helpful!


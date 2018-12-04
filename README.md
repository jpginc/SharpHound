# SharpHound - .xbap wrapper of the C# Rewrite of the BloodHound Ingestor

This project simply wraps the original project (https://github.com/BloodHoundAD/SharpHound) in a .xbap file as an easy way to bypass application whitlist restrictions.

## Building

If you build the project yourself, you might need to move Sharphound2.exe from the Debug\app.publish to the Debug\ folder (overwritting the Sharphound2.exe in the Debug folder). Also you might need to append '.deploy' to the end of the dll/exe files...

## Running
To run bloodhound download the latest release (https://github.com/jpginc/SharpHound/releases)

unzip the debug folder and remove the mark of the internet from Sharphound2.xbap, Sharphound2.exe and Sharphound2.exe.manifest (right click the file -> properties and tick the 'Unlock' checkbox). After removing the mark of the internet double click Sharphound2.xbap or run

    presentationhost.exe "Debug\Sharphound.xbap"
    
In the internet explorer window click the 'run sharphound' button top run win defaults (add command line arguments to the textbox on the left hand side). The window will freeze until the job finished and will display the output in the right hand textbox.


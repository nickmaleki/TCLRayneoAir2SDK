### TCL Rayneo Air 2 unofficial English SDK
Run the .exe located at bin\Debug\net7.0
You can edit the Program.cs file to change the data struct the glasses return
Make sure to rebuild the solution after editing

Be very careful to match the coordinate systems of the Quaternion class type used and the Unity-based Quaternion coordinate system of these augmented reality glasses. 

```
import clr
sys.path.append("TCLRayneoAir2CLI\\bin\\Debug\\net7.0\\")
clr.AddReference("TCLRayneoAir2CLI")
# print(clr._available_namespaces)
from FfalconXR import XRSDK  # syntax highlighter may not highlight errors
myXRSDK = XRSDK()
# print(dir(XRSDK))
myXRSDK.XRSDK_Init()
myXRSDK.reset()
current_glasses_rotation = list(map(float, myXRSDK.ReadArSensors().split(",")))
Quaternion(current_glasses_rotation[3], current_glasses_rotation[0], current_glasses_rotation[1], current_glasses_rotation[2])
```
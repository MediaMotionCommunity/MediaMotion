import os
import sys
import glob

if len(sys.argv) <= 3:
    old = "@loader_path/../lib/libvlccore.8.dylib"
    new = "@loader_path/../../../lib/libvlccore.8.dylib"
else:
    old = sys.argv[2]
    new = sys.argv[3]

for filename in glob.glob("%s/*.dylib" % sys.argv[1]):
    print "Editing", filename
    os.system("install_name_tool -change %s %s %s" % (old, new, filename))

#Some people may have difficulty with dependencies used by NKHook6,
#so this script can take care of all of that for you.
from subprocess import *
from pathlib import Path
import os
import shutil
import git

print("NKHook6 setup script");
print("Checking if Git is installed...");

if call(["git", "branch"], stderr=STDOUT, stdout=open(os.devnull, 'w')) != 0:
    print("Git not found, please install git before building NKHook6!");
    exit;
else:
    print("Git found!");

print("Updating submodules...");
repo_path = os.getcwd();
repo = git.Repo(repo_path);
for submodule in repo.submodules:
    if not (os.path.exists(submodule.name)):
        smPath = Path(submodule.name)
        os.makedirs(smPath);
    print("Updating "+submodule.name);
    submodule.update(init=True, recursive=True, force=True);
print("Deleting NLayer extras...");
shutil.rmtree("NKHook6/NKHook6/Dependencies/NLayer/NLayer.NAudioSupport");
shutil.rmtree("NKHook6/NKHook6/Dependencies/NLayer/TestApp");
print("Creating references...");
os.mkdir("NKHook6/references");
print("Done");

@echo off
echo installing python dependencies...

pip3 install gitpython
pip3 install pathlib

echo starting setup...
py setupvs.py
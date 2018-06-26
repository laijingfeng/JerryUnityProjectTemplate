# !/usr/bin/python
# encoding=utf-8
# version: 2018-06-22 18:47:45

import os
import sys
import stat

if __name__ == '__main__':
    os.chmod("./MapFileParser.sh", stat.S_IRWXU | stat.S_IRGRP | stat.S_IROTH)
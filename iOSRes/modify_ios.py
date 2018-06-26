# !/usr/bin/python
# encoding=utf-8
# version: 2018-06-22 18:47:45
"""
工具模板
"""

import sys
import os
import json
import codecs
import shutil
import subprocess
from logger import Logger

class ExeRsp(object):
    """
    执行命令返回值
    """
    def __init__(self):
        self.returncode = 0  # 返回值
        self.stderr = ''  # 错误

class MainClass(object):
    """
    主类
    """
    def __init__(self):
        """
        初始化
        """
        self.enter_cwd_dir = ''  # 执行路径
        self.python_file_dir = ''  # python文件路径
        self.argv = {}  # 参数，文件名参考：template^k1-v1^k2-v2.py
        self.config = ''  # 配置
        self.log_path = './work'  # 日志文件名
        self.logger = ''  # 日志工具
        self.app_icon_path = ''

    def parse_argv(self):
        """
        解析参数\n
        返回是否成功
        """
        if len(sys.argv) < 1:
            return False
        # 解析文件名参数
        if len(sys.argv) == 1:
            file_name = sys.argv[0]
            file_name = os.path.split(file_name)[1]  # 去掉目录
            file_name = file_name.split('.')[0]  # 去掉后缀
            if file_name.count('^') > 0:
                pars = file_name.split('^', 1)[1]  # 去掉文件名
                self.parse_give_argv(pars)
        else:
            self.parse_give_argv(sys.argv[1])
        return True
    
    def parse_give_argv(self, argvs):
        """
        解析指定参数\n
        独立出来，也可以手动执行
        """
        pars = argvs.split('^')  # 分离参数
        for par in pars:
            if par.find('-') != -1:
                par_key = par.split('-', 1)[0]
                par_val = par.split('-', 1)[1]
                self.argv[par_key] = par_val
    
    def usage(self):
        """
        使用说明，参数不对的时候会提示
        """
        print 'this is usage()'
    
    def __init_data__(self):
        """
        初始化数据，解析参数之后
        """
        self.logger = Logger(Logger.LEVEL_INFO, self.get_exe_path(self.log_path))
        self.logger.reset()
    
    @staticmethod
    def execute_shell_command(args, wait=True):
        """
        执行外部命令\n
        args 参数列表\n
        wait 是否等候
        """
        ret = ExeRsp()
        p = subprocess.Popen(args, stderr=subprocess.PIPE)
        if wait is True:
            ret.returncode = p.wait()
            ret.stderr = p.stderr.read()
            return ret
        else:
            ret.returncode = 0
            return ret

    def to_unicode(self, data):
        """
        数据转unicode
        """
        data = str(data).strip().decode('utf-8')
        return data
    
    def get_exe_path(self, simple_path):
        """
        相对路径转绝对路径
        """
        return os.path.join(self.enter_cwd_dir, self.python_file_dir, simple_path)
    
    def run(self):
        """
        类入口
        """
        success = self.parse_argv()
        if not success:
            self.usage()
            exit(-1)
        self.enter_cwd_dir = os.getcwd()
        self.python_file_dir = os.path.dirname(sys.argv[0])
        self.__init_data__()
        self.work()
    
    def work(self):
        """
        do real work
        """
        
        self.logger.info('start')
        
        if self.argv.has_key("project_name") is False:
            self.logger.info('need par project_name')
            exit(0)

        self.logger.info('project_name is ：' + self.argv["project_name"])

        self.app_icon_path = self.get_exe_path('./../' + self.argv["project_name"] + '/Unity-iPhone/Images.xcassets/AppIcon.appiconset/')
        json_file = os.path.join(self.app_icon_path ,'./Contents.json')
        
        if os.path.exists(json_file) is False:
            self.logger.info('project not exist')
            exit(0)
        
        image_file = os.path.join(self.app_icon_path ,'./Icon-AppStore.png')
        shutil.copy(self.get_exe_path('./Icon-AppStore.png'), image_file)

        with codecs.open(json_file, 'r', 'utf-8') as file_handle:
            self.config = json.load(file_handle)
        
        app_store_image = {}
        app_store_image["filename"] = u'Icon-AppStore.png'
        app_store_image["idiom"] = u'ios-marketing'
        app_store_image["scale"] = u'1x'
        app_store_image["size"] = u'1024x1024'

        self.config["images"].append(app_store_image)

        with codecs.open(json_file, 'w', 'utf-8') as file_handle:
            config_str = json.dumps(self.config)
            file_handle.write(config_str)
        
        self.logger.info('finish')

if __name__ == '__main__':
    reload(sys)
    sys.setdefaultencoding('utf-8')

    MAIN_CLASS = MainClass()
    MAIN_CLASS.run()

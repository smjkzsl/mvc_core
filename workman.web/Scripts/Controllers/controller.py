import clr
#clr.AddReference("Workman.DataBase.Repository");
#clr.AddReference("Workman.DataBase");
#from Workman.DataBase import *
#from Workman.DataBase.Repository import *
 
class Controller(object):
    mvcInstance = 0
    request = 0
    response = 0
    session = 0
    server = None
    def __init__(self):
        pass
    @property
    def db(self):
        return RepositoryFactory().BaseRepository()
    @property
    def view(self):
        return self.__MVC;
    @property
    def viewbag(self):
        #self.mvcInstance.SetViewBag(name,data)
        return self.mvcInstance.GetViewBag()
    def Log(self,info):
        self.mvcInstance.WriteLog(str(info))
    def ErrorLog(self,info):
        self.mvcInstance.WriteLog(str(info),4)
    def initMVC(self,mvcInstance):
        self.mvcInstance = mvcInstance
        self.request = mvcInstance.GetRequest()
        self.response = mvcInstance.GetResponse()
        self.session = mvcInstance.GetSession()
        #self.server = mvcInstance.GetServer()
    def CsHtml(self,data=None):
        return self.mvcInstance.ShowView(data)
    def Json(self,data):
        return self.mvcInstance.ShowJson(data)
    def Javascript(self,data):
        return self.mvcInstance.ShowJavascript(data)
    def Success(self,data):
        return self.mvcInstance.Success(data)
    def Fail(self,data):
        return self.mvcInstance.Fail(data)
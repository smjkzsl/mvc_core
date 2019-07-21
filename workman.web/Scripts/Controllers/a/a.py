from controller import Controller
class a(Controller): 
	def Index(self,id=""):
		self.viewbag.p='ok'
		return self.CsHtml()
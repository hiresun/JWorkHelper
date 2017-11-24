
>1. 自己动手写的ado.net 的一个工具，根据数据库表自动生成Model,自动生成insert，update,delete,select，以及导出Excel的方法。
>2. 涉及知识主要是ado.net、winform,sql基本无难点。
>3. 主要解决公司webform大量重复机械的动作，由于本人太懒，所以就写了个工具。
>4. 另外还包括，写日志，审批认可和否决操作代码生成，SQL分页方法生成，Excel导出代码生成手动添加字段实体等映射关系等扩展。
就这样，具体看代码。
>5. 添加了T4模板根据数据库自动生成实体类模板，另外还有生成insert的方法，可以自行在此基础上扩展select，update，delete的方法。
>6. 另外还有数据库存储过程生成实体类的方法，[博客地址](http://www.cnblogs.com/anyushengcms/p/7573289.html)：http://www.cnblogs.com/anyushengcms/p/7573289.html

添加了安装包，直接点击下一步下一步就可以了，在桌面生成工具，点击工具可以用了

![安装包.png](http://upload-images.jianshu.io/upload_images/6855212-60c3dd32953cafe8.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
### WorkHelper

先预览界面，winform 就不在乎美观不美观了。

![1.png](http://upload-images.jianshu.io/upload_images/6855212-c7cec73af2b5df43.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![2.png](http://upload-images.jianshu.io/upload_images/6855212-ab7e228980c7a6f9.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![3.png](http://upload-images.jianshu.io/upload_images/6855212-5cfecc9619ccde4c.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![4.png](http://upload-images.jianshu.io/upload_images/6855212-d4315847e95dd79e.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![5.png](http://upload-images.jianshu.io/upload_images/6855212-19b78ad07e6e674a.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![6.png](http://upload-images.jianshu.io/upload_images/6855212-0ae716cc63a28065.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

扩展存储过程版本：
调用存储过程：    EXEC [dbo].[p_db_wsp] '数据库名字', '保存的路径：D:\work\新建文件夹', '生成实体类名字';
另外：[github地址](https://github.com/Jimmey-Jiang/JWorkHelper)：https://github.com/Jimmey-Jiang/JWorkHelper

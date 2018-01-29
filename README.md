
## 简介
>1. 自己动手写的ado.net 的一个工具，根据数据库表自动生成Model,自动生成insert，update,delete,select，以及导出Excel的方法。
>2. 涉及知识主要是ado.net、winform,sql基本无难点。
>3. 主要解决公司webform大量重复机械的动作，由于本人太懒，所以就写了个工具。
>4. 另外还包括，写日志，审批认可和否决操作代码生成，SQL分页方法生成，Excel导出代码生成手动添加字段实体等映射关系等扩展。
就这样，具体看代码。
>5. 添加了T4模板根据数据库自动生成实体类模板，另外还有生成insert的方法，可以自行在此基础上扩展select，update，delete的方法。
>6. 另外还有数据库存储过程生成实体类的方法，[博客地址](http://www.cnblogs.com/anyushengcms/p/7573289.html)：http://www.cnblogs.com/anyushengcms/p/7573289.html
>7. 新增了安装包，直接安装可用，不用打开程序。

## 运行效果
SQL语句：
![image.png](http://upload-images.jianshu.io/upload_images/6855212-8b2170cbcb7facb5.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

![image.png](http://upload-images.jianshu.io/upload_images/6855212-ad20f719ead84f6f.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

![image.png](http://upload-images.jianshu.io/upload_images/6855212-ad9edc1b977cfcc7.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

![image.png](http://upload-images.jianshu.io/upload_images/6855212-3ec533671adcbdf0.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

## 使用方法：
修改自己数据库连接，然后生成，bin目录下找到DevLogHelper.exe文件，即可。

![image.png](http://upload-images.jianshu.io/upload_images/6855212-8c1c32eb253f2e8f.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


![安装包.png](http://upload-images.jianshu.io/upload_images/6855212-60c3dd32953cafe8.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


## 扩展界面
先预览界面，winform 就不在乎美观不美观了。
![2.png](http://upload-images.jianshu.io/upload_images/6855212-ab7e228980c7a6f9.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![3.png](http://upload-images.jianshu.io/upload_images/6855212-5cfecc9619ccde4c.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![4.png](http://upload-images.jianshu.io/upload_images/6855212-d4315847e95dd79e.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![5.png](http://upload-images.jianshu.io/upload_images/6855212-19b78ad07e6e674a.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![6.png](http://upload-images.jianshu.io/upload_images/6855212-0ae716cc63a28065.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
![1.png](http://upload-images.jianshu.io/upload_images/6855212-c7cec73af2b5df43.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)



### WorkHelper
扩展存储过程版本：
调用存储过程：    EXEC [dbo].[p_db_wsp] '数据库名字', '保存的路径：D:\work\新建文件夹', '生成实体类名字';
另外：[github地址](https://github.com/Jimmey-Jiang/JWorkHelper)：https://github.com/Jimmey-Jiang/JWorkHelper

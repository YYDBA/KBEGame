﻿
//读CSV文件类,读取指定的CSV文件，可以导出DataTable
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;

public class CsvStreamReader
{
    private ArrayList rowAL;         //行链表,CSV文件的每一行就是一个链
    private string fileName;        //文件名
    private string pathDir;
    private string shotName;

    private Encoding encoding;        //编码
    public CsvStreamReader()
    {
        this.rowAL = new ArrayList();
        this.fileName = "";
        this.encoding = Encoding.Default;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="fileName">文件名,包括文件路径</param>
    public CsvStreamReader(string fileName)
    {
        this.rowAL = new ArrayList();
        this.fileName = fileName;
        this.encoding = Encoding.Default;
        int idx = fileName.LastIndexOf(@"\") + 1;
        pathDir = fileName.Substring(0, idx-7);
        shotName = fileName.Substring(idx, fileName.Length - idx - 4);
        LoadCsvFile();
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="fileName">文件名,包括文件路径</param>
    /// <param name="encoding">文件编码</param>
    public CsvStreamReader(string fileName, Encoding encoding)
    {
        this.rowAL = new ArrayList();
        this.fileName = fileName;
        this.encoding = encoding;
        LoadCsvFile();
    }
    /// <summary>
    /// 文件名,包括文件路径
    /// </summary>
    public string FileName
    {
        set
        {
            this.fileName = value;
            LoadCsvFile();
        }
    }
    /// <summary>
    /// 文件编码
    /// </summary>
    public Encoding FileEncoding
    {
        set
        {
            this.encoding = value;
        }
    }
    /// <summary>
    /// 获取行数
    /// </summary>
    public int RowCount
    {
        get
        {
            return this.rowAL.Count;
        }
    }
    /// <summary>
    /// 获取列数
    /// </summary>
    public int ColCount
    {
        get
        {
            int maxCol;
            maxCol = 0;
            for (int i = 0; i < this.rowAL.Count; i++)
            {
                ArrayList colAL = (ArrayList)this.rowAL[i];
                maxCol = (maxCol > colAL.Count) ? maxCol : colAL.Count;
            }
            return maxCol;
        }
    }
    /// <summary>
    /// 获取某行某列的数据
    /// row:行,row = 1代表第一行
    /// col:列,col = 1代表第一列  
    /// </summary>
    public string this[int row, int col]
    {
        get
        {
            //数据有效性验证
            CheckRowValid(row);
            CheckColValid(col);
            ArrayList colAL = (ArrayList)this.rowAL[row - 1];
            //如果请求列数据大于当前行的列时,返回空值
            if (colAL.Count < col)
            {
                return "";
            }
            return colAL[col - 1].ToString();
        }
    }
    /// <summary>
    /// 根据最小行，最大行，最小列，最大列，来生成一个DataTable类型的数据
    /// 行等于1代表第一行
    /// 列等于1代表第一列
    /// maxrow: -1代表最大行
    /// maxcol: -1代表最大列
    /// </summary>
    public DataTable this[int minRow, int maxRow, int minCol, int maxCol]
    {
        get
        {
            //数据有效性验证
            CheckRowValid(minRow);
            CheckMaxRowValid(maxRow);
            CheckColValid(minCol);
            CheckMaxColValid(maxCol);
            if (maxRow == -1)
            {
                maxRow = RowCount;
            }
            if (maxCol == -1)
            {
                maxCol = ColCount;
            }
            if (maxRow < minRow)
            {
                PrintLog("最大行数不能小于最小行数");
            }
            if (maxCol < minCol)
            {
                PrintLog("最大列数不能小于最小列数");
            }
            DataTable csvDT = new DataTable();
            int i;
            int col;
            int row;
            //增加列
            for (i = minCol; i <= maxCol; i++)
            {
                csvDT.Columns.Add(i.ToString());
            }
            for (row = minRow; row <= maxRow; row++)
            {
                DataRow csvDR = csvDT.NewRow();
                i = 0;
                for (col = minCol; col <= maxCol; col++)
                {
                    csvDR[i] = this[row, col];
                    i++;
                }
                csvDT.Rows.Add(csvDR);
            }
            return csvDT;
        }
    }
    /// <summary>
    /// 检查行数是否是有效的
    /// </summary>
    /// <param name="col"></param>  
    private void CheckRowValid(int row)
    {
        if (row <= 0)
        {
            PrintLog("行数不能小于0");
        }
        if (row > RowCount)
        {
            PrintLog("没有当前行的数据");
        }
    }
    /// <summary>
    /// 检查最大行数是否是有效的
    /// </summary>
    /// <param name="col"></param>  
    private void CheckMaxRowValid(int maxRow)
    {
        if (maxRow <= 0 && maxRow != -1)
        {
            PrintLog("行数不能等于0或小于-1");
        }
        if (maxRow > RowCount)
        {
            PrintLog("没有当前行的数据");
        }
    }
    /// <summary>
    /// 检查列数是否是有效的
    /// </summary>
    /// <param name="col"></param>  
    private void CheckColValid(int col)
    {
        if (col <= 0)
        {
            PrintLog("列数不能小于0");
        }
        if (col > ColCount)
        {
            PrintLog("没有当前列的数据");
        }
    }
    /// <summary>
    /// 检查检查最大列数是否是有效的
    /// </summary>
    /// <param name="col"></param>  
    private void CheckMaxColValid(int maxCol)
    {
        if (maxCol <= 0 && maxCol != -1)
        {
            PrintLog("列数不能等于0或小于-1");
        }
        if (maxCol > ColCount)
        {
            PrintLog("没有当前列的数据");
        }
    }
    /// <summary>
    /// 载入CSV文件
    /// </summary>
    private void LoadCsvFile()
    {
        //对数据的有效性进行验证
        if (this.fileName == null)
        {
            PrintLog("请指定要载入的CSV文件名");
        }
        else if (!File.Exists(this.fileName))
        {
            PrintLog("指定的CSV文件不存在");
        }
       
        if (this.encoding == null)
        {
            this.encoding = Encoding.Default;
        }
        StreamReader sr = new StreamReader(this.fileName, this.encoding);
        string csvDataLine;
        csvDataLine = "";
        string fileDataLine = sr.ReadLine();
        while (true)
        {
            fileDataLine = sr.ReadLine();
            if (fileDataLine == null)
            {
                break;
            }
            if (csvDataLine == "")
            {
                csvDataLine = fileDataLine;//GetDeleteQuotaDataLine(fileDataLine);
            }
            else
            {
                csvDataLine += "\\r\\n" + fileDataLine;//GetDeleteQuotaDataLine(fileDataLine);
            }
            //如果包含偶数个引号，说明该行数据中出现回车符或包含逗号
            if (!IfOddQuota(csvDataLine))
            {
                AddNewDataLine(csvDataLine);
                csvDataLine = "";
            }
        }
        sr.Close();
        //数据行出现奇数个引号
        if (csvDataLine.Length > 0)
        {
            PrintLog("CSV文件的格式有错误");
        }
        GenCSFile();
        GenBytesFile();
    }
    /// <summary>
    /// 获取两个连续引号变成单个引号的数据行
    /// </summary>
    /// <param name="fileDataLine">文件数据行</param>
    /// <returns></returns>
    private string GetDeleteQuotaDataLine(string fileDataLine)
    {
        return fileDataLine.Replace("\"\"", "\"");
    }
    /// <summary>
    /// 判断字符串是否包含奇数个引号
    /// </summary>
    /// <param name="dataLine">数据行</param>
    /// <returns>为奇数时，返回为真；否则返回为假</returns>
    private bool IfOddQuota(string dataLine)
    {
        int quotaCount;
        bool oddQuota;
        quotaCount = 0;
        for (int i = 0; i < dataLine.Length; i++)
        {
            if (dataLine[i] == '\"')
            {
                quotaCount++;
            }
        }
        oddQuota = false;
        if (quotaCount % 2 == 1)
        {
            oddQuota = true;
        }
        return oddQuota;
    }
    /// <summary>
    /// 判断是否以奇数个引号开始
    /// </summary>
    /// <param name="dataCell"></param>
    /// <returns></returns>
    private bool IfOddStartQuota(string dataCell)
    {
        int quotaCount;
        bool oddQuota;
        quotaCount = 0;
        for (int i = 0; i < dataCell.Length; i++)
        {
            if (dataCell[i] == '\"')
            {
                quotaCount++;
            }
            else
            {
                break;
            }
        }
        oddQuota = false;
        if (quotaCount % 2 == 1)
        {
            oddQuota = true;
        }
        return oddQuota;
    }
    /// <summary>
    /// 判断是否以奇数个引号结尾
    /// </summary>
    /// <param name="dataCell"></param>
    /// <returns></returns>
    private bool IfOddEndQuota(string dataCell)
    {
        int quotaCount;
        bool oddQuota;
        quotaCount = 0;
        for (int i = dataCell.Length - 1; i >= 0; i--)
        {
            if (dataCell[i] == '\"')
            {
                quotaCount++;
            }
            else
            {
                break;
            }
        }
        oddQuota = false;
        if (quotaCount % 2 == 1)
        {
            oddQuota = true;
        }
        return oddQuota;
    }
    /// <summary>
    /// 加入新的数据行
    /// </summary>
    /// <param name="newDataLine">新的数据行</param>
    private void AddNewDataLine(string newDataLine)
    {
        //System.Diagnostics.Debug.WriteLine("NewLine:" + newDataLine);
        ////return;
        ArrayList colAL = new ArrayList();
        string[] dataArray = newDataLine.Split(',');
        bool oddStartQuota;        //是否以奇数个引号开始
        string cellData;
        oddStartQuota = false;
        cellData = "";
        for (int i = 0; i < dataArray.Length; i++)
        {
            if (oddStartQuota)
            {
                //因为前面用逗号分割,所以要加上逗号
                cellData += "," + dataArray[i];
                //是否以奇数个引号结尾
                if (IfOddEndQuota(dataArray[i]))
                {
                    colAL.Add(GetHandleData(cellData));
                    oddStartQuota = false;
                    continue;
                }
            }
            else
            {
                //是否以奇数个引号开始
                if (IfOddStartQuota(dataArray[i]))
                {
                    //是否以奇数个引号结尾,不能是一个双引号,并且不是奇数个引号
                    if (IfOddEndQuota(dataArray[i]) && dataArray[i].Length > 2 && !IfOddQuota(dataArray[i]))
                    {
                        colAL.Add(GetHandleData(dataArray[i]));
                        oddStartQuota = false;
                        continue;
                    }
                    else
                    {
                        oddStartQuota = true;
                        cellData = dataArray[i];
                        continue;
                    }
                }
                else
                {
                    colAL.Add(GetHandleData(dataArray[i]));
                }
            }
        }
        if (oddStartQuota)
        {
            PrintLog("数据格式有问题");
        }
        this.rowAL.Add(colAL);
    }
    /// <summary>
    /// 去掉格子的首尾引号，把双引号变成单引号
    /// </summary>
    /// <param name="fileCellData"></param>
    /// <returns></returns>
    private string GetHandleData(string fileCellData)
    {
        if (fileCellData == "")
        {
            return "";
        }
        if (IfOddStartQuota(fileCellData))
        {
            if (IfOddEndQuota(fileCellData))
            {
                return fileCellData.Substring(1, fileCellData.Length - 2).Replace("\"\"", "\""); //去掉首尾引号，然后把双引号变成单引号
                }
            else
            {
                PrintLog("数据引号无法匹配" + fileCellData);
            }
        }
        else
        {
            //考虑形如""    """"      """"""   
            if (fileCellData.Length > 2 && fileCellData[0] == '\"')
            {
                fileCellData = fileCellData.Substring(1, fileCellData.Length - 2).Replace("\"\"", "\""); //去掉首尾引号，然后把双引号变成单引号
            }
        }
        return fileCellData;
    }

    private void GenCSFile()
    {
        if (shotName.Equals("Language"))
        {
            return;
        }
        else
        {
            string csPath = pathDir + "CS/" + shotName + ".cs";
            ArrayList colAL0 = (ArrayList)this.rowAL[0];
            ArrayList colAL1 = (ArrayList)this.rowAL[1];
            StringBuilder sb = new StringBuilder();
            sb.Append("public class ");
            sb.Append(shotName);
            sb.Append("{\r\n");
            for (int i = 0;i < ColCount;++i)
            {
                sb.Append("\t");
                sb.Append("public ");
                sb.Append(colAL1[i]);
                sb.Append(" ");
                sb.Append(colAL0[i]);
                sb.Append(";\r\n");
            }
            sb.Append("}");
            StreamWriter sw = new StreamWriter(csPath);
            sw.Write(sb);
            sw.Close();
        }
    }

    private void GenBytesFile()
    {
        string splitCode = ((char)0x02).ToString();

        if (shotName.Equals("Language"))
        {
            ArrayList col0 = (ArrayList)this.rowAL[0];
            for (int j = 1;j < col0.Count;++j)
            {
                string _name = col0[j].ToString();
                string str = "";
                for (int i = 1; i < RowCount; ++i)
                {
                    ArrayList col = (ArrayList)this.rowAL[i];
                    str += col[0]+ splitCode + col[j] + "\r\n";
                }
                string bytePath = pathDir + "Bytes/"+ _name + ".bytes";
                if (File.Exists(bytePath))
                {
                    File.Delete(bytePath);
                }
                FileStream fs = null;
                using (fs = new FileStream(bytePath, FileMode.OpenOrCreate))
                {
                    byte[] bitArr = System.Text.Encoding.UTF8.GetBytes(str);
                    fs.Write(bitArr, 0, bitArr.Length);
                    fs.Flush();
                }
            }
        }
        else
        {
            string str = "";
            for(int i = 2; i < RowCount; ++i)
            {
                ArrayList col = (ArrayList)this.rowAL[i];
                int j = 0;
                for (; j < ColCount - 1; ++j)
                {
                    str += col[j] + splitCode;
                }
                str += col[j]+"\r\n";
            }
            string bytePath = pathDir + "Bytes/"+ shotName + ".bytes";
            if (File.Exists(bytePath))
            {
                File.Delete(bytePath);
            }
            FileStream fs = null;
            using (fs = new FileStream(bytePath,FileMode.OpenOrCreate))
            {
                byte[] bitArr = System.Text.Encoding.UTF8.GetBytes(str);
                fs.Write(bitArr,0,bitArr.Length);
                fs.Flush();
            }
        }
        PrintLog("完成!",true);
    }

    void PrintLog(string msg,bool state = false)
    {
        if (state)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(shotName +":"+ msg);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(shotName + ":" + msg);
            throw new Exception(msg);
        }
    }
}
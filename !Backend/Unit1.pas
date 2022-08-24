unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, DB, ADODB,IdSNMP,IdIcmpClient, ExtCtrls, ComCtrls,
  Menus, RXShell;

type
  TForm1 = class(TForm)
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    Button7: TButton;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button6: TButton;
    Timer1: TTimer;
    Edit1: TEdit;
    Label1: TLabel;
    Button4: TButton;
    CheckBox1: TCheckBox;
    RxTrayIcon1: TRxTrayIcon;
    PopupMenu1: TPopupMenu;
    N1: TMenuItem;
    N2: TMenuItem;
    Edit3: TEdit;
    Label3: TLabel;
    CheckBox2: TCheckBox;
    Button5: TButton;
    Button8: TButton;
    Memo1: TMemo;
    Label2: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Button7Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure N2Click(Sender: TObject);
    procedure N1Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);
    
  private
    { Private declarations }
    procedure onminize(var Message:tmessage); message WM_SYSCOMMAND;
  public
    { Public declarations }
    procedure InsertSQLTable(nameprn,modelprn,ipprn,snprn,prnpages,prncolorpages,dateprn,timeprn,datetimeprn:string);
    procedure WriteDebugLog(s: AnsiString);
  end;                                                                                                                  

type
TNewThread = class(TThread)
  private
    AHost : string;
    ATimes : integer;
    AvgMS:Double;
    List:TStringList;
    procedure SetProgress;
  protected
    procedure Execute; override;

  end;

var
  Form1: TForm1;
  NameSQLBase,NameSQLTable,NameSQLTable2,NameSQLServer:string;
  dir_app:string;
  NewThread: TNewThread;
  errorstate,devicestatus,hostname,status, sost,sn,pages,colorpages,toner,toner2,cart:string[100];
  tonerBlue1,tonerBlue2,tonerYellow1,tonerYellow2,tonerMagenta1,tonerMagenta2:string[100];
  PrnList:TStringList;
  b_time,b_date:boolean;

implementation

{$R *.dfm}


procedure TForm1.onminize(var Message:tmessage);
begin
if message.WParam=SC_MINIMIZE then
 begin
      ShowWindow(Form1.Handle,SW_HIDE);
      ShowWindow(Application.Handle, SW_HIDE);
      Application.ShowMainForm := False;
      Application.MainForm.Hide;
      Form1.PopupMenu1.Items[0].Caption:='Развернуть';
 end
else
  inherited;
end;

{ Функция SNMP опроса принтера }
Function SNMPQuery (Host, Mib: string) : string;
  var
      s: String;
      i, j: Integer;
      SNMP: TIdSNMP;
      temps:string;
begin
    SNMP := TIdSNMP.Create(nil);
    SNMP.Query.Host := Host;
    SNMP.Query.Port := 161;
    SNMP.Query.Community := 'public';
    SNMP.Query.PDUType := PDUGetRequest;
    SNMP.Query.MIBAdd(Mib,'');
    try
      if SNMP.SendQuery then
        for i := 0 to SNMP.Reply.ValueCount - 1 do
         begin
          SNMPQuery:= SNMP.Reply.Value[i];
          temps:=SNMP.Reply.Value[0]
         end;
    finally
          Application.ProcessMessages;
          SNMP.Free;
    end;
end;

function HexToInt(s: string): string;
var
  temps:string ;
  i: integer;
begin

  if s = '' then
  begin
    HexToInt := '0';
    exit;
  end;
  for i := 1 to Length(s) do
    temps := temps + IntToHex(Ord(s[i]), 2);
  Result:=temps;
end;

Function case_mb(s:string):boolean;
begin
  Result:=false;
  if (Pos('hp',lowerCase(s))>0)  then   //  or (Pos('1522',s)>0) or (Pos('1530',s)>0) or (Pos('2025',s)>0)
   begin
       Result:=true;
       sost:= '1.3.6.1.2.1.43.17.6.1.5.1.2';
       sn:= '1.3.6.1.2.1.43.5.1.1.17.1';
       pages:= '1.3.6.1.2.1.43.10.2.1.4.1.1';
       colorpages:='1.3.6.1.2.1.43.10.2.1.4.1.1';;
       toner:='1.3.6.1.2.1.43.11.1.1.9.1.1';
       toner2:='1.3.6.1.2.1.43.11.1.1.8.1.1';
       cart:='1.3.6.1.2.1.43.11.1.1.6.1.1';
       status:= '1.3.6.1.2.1.43.17.6.1.5.1.1';
       hostname:='1.3.6.1.2.1.1.5.0';
       devicestatus:='1.3.6.1.2.1.25.3.2.1.5.1'; //unknown(1), running(2), warning(3), testing(4), down(5)
       errorstate:='1.3.6.1.2.1.25.3.5.1.2.1';
       if (Pos('color',lowerCase(s))>0) then
         begin
           tonerBlue1:='1.3.6.1.2.1.43.11.1.1.9.1.2';
           tonerBlue2:='1.3.6.1.2.1.43.11.1.1.8.1.2';
           tonerYellow1:='1.3.6.1.2.1.43.11.1.1.9.1.4';
           tonerYellow2:='1.3.6.1.2.1.43.11.1.1.8.1.4';
           tonerMagenta1:='1.3.6.1.2.1.43.11.1.1.9.1.3';
           tonerMagenta2:='1.3.6.1.2.1.43.11.1.1.8.1.3';
          // Form7.Gauge2.Visible:=true;
          // Form7.Gauge3.Visible:=true;
          // Form7.Gauge4.Visible:=true;
         end
            else
          begin
           tonerBlue1:='';
           tonerBlue2:='';
           tonerYellow1:='';
           tonerYellow2:='';
           tonerMagenta1:='';
           tonerMagenta2:='';
          end;
   end;
end;

//Запись лога отладки

function StringToWideString(const s: AnsiString; codePage: Word): WideString;
var
  l: integer;
begin
  if s = '' then
    Result := ''
else
  begin
    l := MultiByteToWideChar(codePage, MB_PRECOMPOSED, PansiChar(@s[1]), -1, nil,
      0);
    SetLength(Result, l - 1);
    if l > 1 then
      MultiByteToWideChar(CodePage, MB_PRECOMPOSED, PansiChar(@s[1]),
        -1, PWideChar(@Result[1]), l - 1);
  end;
end;

procedure TForm1.WriteDebugLog(s: AnsiString);
var
  hFile: THandle;
  Dummy: Cardinal;
  namefile: String;
begin
try
  namefile:=dir_app+'\Debug.log';
  hFile:=CreateFile(PChar(namefile), GENERIC_WRITE, 0, nil, OPEN_ALWAYS, 0, 0);
  if hFile<>INVALID_HANDLE_VALUE then
  begin
    SetFilePointer(hFile, 0, nil, FILE_END);
    s:=DateTimeToStr(Now)+': '+s+#13#10;
    WriteFile(hFile, PChar(s)^, Length(s), Dummy, nil);
    CloseHandle(hFile);
  end;
except
   Form1.WriteDebugLog('Form1: WriteDebugLog: Error');
end;  
end;

procedure TNewThread.Execute;
  var
    R : array of Cardinal;
    i ,j: integer;
    res:boolean;
    mib:array [1..15] of string[80];
begin
  while not Terminated do
    begin
      Form1.Memo1.Lines.Append(DateToStr(now) + ' ' + TimeToStr(now)+ ' Запущен поток обновления!');
      Form1.WriteDebugLog('Количество принтеров: ' + IntTOStr(List.Count));
      For j:=0 to List.Count-1 do
        begin
            Res := True;
            AvgMS := 0;
            if ATimes>0 then
              with TIdIcmpClient.Create(Form1) do
              try
                  Form1.WriteDebugLog('Опрашивается принтер: ' + List[j]);
                  Host := List[j];
                  AHost:=Host;
                  ReceiveTimeout:=500; //TimeOut du ping
                  SetLength(R,ATimes);
                  {Pinguer le client}
                  for i:=0 to Pred(ATimes) do
                  begin
                      try
                        Ping();
                        Application.ProcessMessages; //ne bloque pas l'application
                        R[i] := ReplyStatus.MsRoundTripTime;
                      except
                        Res := False;
                        Exit;

                      end;
                    if ReplyStatus.ReplyStatusType<>rsEcho Then res := False; //pas d'e'cho, on renvoi false.
                  end;
                  {Faire une moyenne}
                  for i:=Low(R) to High(R) do
                  begin
                    Application.ProcessMessages;
                    AvgMS := AvgMS + R[i];
                  end;
                  AvgMS := AvgMS / i;
              finally
                  Free;
              end;
              if res then
               begin
                        Synchronize(SetProgress);
               end ;
         end;
        Terminate; // Останавливаем поток.
    end;
    Form1.Label2.Caption:='Конец обновления: '+DateToStr(now) + ' ' +TimeToStr(now);
    SendMessage(Application.Handle, WM_NULL, 0, 0); // Дергаем главный поток.
end;

procedure TNewThread.SetProgress;
const PBM_SETBARCOLOR = WM_USER+9;
 var
  i:integer;
  kol,kol2,errst,devst:string;
  prn_sn, prn_pages,prn_color_pages,prn_hostname,prn_model:string;
  time1:TTime;
  date1:TDate;

begin
    Form1.Label2.Caption:='Проверяется принтер: ' + AHost;
    try
        if case_mb(SNMPQuery(AHost,'1.3.6.1.2.1.25.3.2.1.3.1')) then
         begin
                prn_sn:=SNMPQuery(AHost,sn);
                prn_pages:=SNMPQuery(AHost,pages);
                prn_color_pages:=SNMPQuery(AHost,colorpages);
                prn_hostname:=SNMPQuery(AHost,hostname);
                prn_model:=SNMPQuery(AHost,'1.3.6.1.2.1.25.3.2.1.3.1');
                date1:=now;
                time1:=now;
                try
                  Form1.InsertSQLTable(prn_hostname,prn_model,AHost,prn_sn,prn_pages,prn_color_pages,DateToStr(date1),TimeToStr(time1),DateTimeToStr(now));
                except
                  Form1.WriteDebugLog('Ошибка данных принтера: ' + AHost);
                end;
                Form1.WriteDebugLog('Получены данные с принтера : ' + AHost+ '('+prn_hostname+')');
         end;
    except
      Form1.WriteDebugLog('Ошибка данных принтера (main): ' + AHost);
    end;

end;

procedure CreateSQLBase;
var myADO : TADOQuery;
begin
myADO:=TADOQuery.Create(Form1);
myADO.ConnectionString:='Provider=SQLOLEDB.1;'+
'Integrated Security=SSPI;'+
'Persist Security Info=False;'+
//'Initial Catalog=prnBase;'+
'Data Source='+NameSQLServer;
myADO.SQL.Clear;
myADO.SQL.Text:='CREATE DATABASE '+NameSQLBase;
try
myADO.ExecSQL;
except
ShowMessage('База данных с именем: "'+NameSQLBase+'" - уже существует...');
end;
myADO.Free;
end;


procedure CreateSQLTable;
  var myADO : TADOQuery;
begin
    myADO:=TADOQuery.Create(Form1);
    myADO.ConnectionString:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog='+NameSQLBase+';'+
    'Data Source='+NameSQLServer;
    myADO.SQL.Clear;
    myADO.SQL.Text:='CREATE TABLE '+ NameSQLTable + ' (Имя_принтера char(60), Модель_принтера char(100), IP char(20), Серийный_номер char(35), Количество_страниц int, Количество_цветных_страниц int, Дата_проверки date, Время_проверки time, ДатаВремя_проверки datetime)';
    try
        myADO.ExecSQL;
    except
        ShowMessage('Ошибка создания таблицы "'+NameSQLTable+'", или'+#13+#10+
        'такая таблица уже существует...');
    end;
    myADO.Free;
end;

procedure CreateSQLTable_2;
  var myADO : TADOQuery;
begin
    myADO:=TADOQuery.Create(Form1);
    myADO.ConnectionString:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog='+NameSQLBase+';'+
    'Data Source='+NameSQLServer;
    myADO.SQL.Clear;
    myADO.SQL.Text:='CREATE TABLE spprinter (Код int, Имя_принтера char(60), Группа_принтера char(150), IP char(20), Модель char(65), Дата_добавления date, Время_добавления time)';
    try
        myADO.ExecSQL;
    except
        ShowMessage('Ошибка создания таблицы "'+NameSQLTable+'", или'+#13+#10+
        'такая таблица уже существует...');
    end;
    myADO.Free;
end;

procedure CreateSQLTable_group;
  var myADO : TADOQuery;
begin
    myADO:=TADOQuery.Create(Form1);
    myADO.ConnectionString:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog='+NameSQLBase+';'+
    'Data Source='+NameSQLServer;
    myADO.SQL.Clear;
    myADO.SQL.Text:='CREATE TABLE ListOfGroup (Код int, Имя_группы char(150))';
    try
        myADO.ExecSQL;
    except
        ShowMessage('Ошибка создания таблицы ListOfGroup!');
    end;
    myADO.Free;
end;

procedure CreateSQLTable_Model;
  var myADO : TADOQuery;
begin
    myADO:=TADOQuery.Create(Form1);
    myADO.ConnectionString:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog='+NameSQLBase+';'+
    'Data Source='+NameSQLServer;
    myADO.SQL.Clear;
    myADO.SQL.Text:='CREATE TABLE ModelOID (NameOfModel char(150),ProfileOfModel char(150),OIDOfModel char(150),ValueOfModelOID char(150))';
    try
        myADO.ExecSQL;
    except
        ShowMessage('Ошибка создания таблицы ListOfGroup!');
    end;
    myADO.Free;
end;

procedure CreateSQLTable_Profile;
  var myADO : TADOQuery;
begin
    myADO:=TADOQuery.Create(Form1);
    myADO.ConnectionString:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog='+NameSQLBase+';'+
    'Data Source='+NameSQLServer;
    myADO.SQL.Clear;
    myADO.SQL.Text:='CREATE TABLE PrnProfile (NameOfProfile char(150),BlackToner char(100),CyanToner char(100),MagentaToner char(100),'+
    'YellowToner char(100),Location char(100),Description char(100),MAC char(100),SerialNumber char(100),'+
    'Hostname char(100),PageCount char(100),)';
    try
        myADO.ExecSQL;
    except
        ShowMessage('Ошибка создания таблицы ListOfGroup!');
    end;
    myADO.Free;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
CreateSQLBase;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
NameSQLServer:=Form1.Edit3.Text;
NameSQLBase:='prnBase';
NameSQLTable:='printers';
NameSQLTable2:='spprinter';
dir_app:=ExtractFileDir(Application.ExeName);

end;

procedure TForm1.Button2Click(Sender: TObject);
begin
CreateSQLTable;
end;

procedure InsertSQLTable_2(kod:integer;nameprn,groupprn,ipprn,modelprn,dateprn,timeprn:string);
  var
   myADO2 : TADOQuery;
   QryText:string;
Begin
    myADO2:=TADOQuery.Create(Form1);
    myADO2.ConnectionString:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog='+NameSQLBase+';'+
    'Data Source='+NameSQLServer;          

    myADO2.SQL.Clear;
    myADO2.SQL.Text:='Insert INTO '+NameSQLTable2+' (Код, Имя_принтера, Группа_принтера, IP, Модель, Дата_добавления, Время_добавления) '+
    'VALUES (:Код, :Имя_принтера,:Группа_принтера, :IP, :Модель, :Дата_добавления,:Время_добавления)';
    //myADO2.SQL.Text:=Format(QryText, [NameSQLTable2]);
    myADO2.Parameters.ParamByName('Код').Value := kod;
    myADO2.Parameters.ParamByName('Имя_принтера').Value := nameprn;
    myADO2.Parameters.ParamByName('Группа_принтера').value := groupprn;
    myADO2.Parameters.ParamByName('IP').value := ipprn;
    myADO2.Parameters.ParamByName('Модель').value := modelprn;
    myADO2.Parameters.ParamByName('Дата_добавления').value := StrToDate(dateprn);
    myADO2.Parameters.ParamByName('Время_добавления').value := StrToTime(timeprn);
    try
        myADO2.ExecSQL;
    except
        //ShowMessage('Ошибка создания таблицы "'+NameSQLTable+'", или'+#13+#10+  'такая таблица уже существует...');
        Form1.WriteDebugLog('Ошибка добавления в таблицу: '+NameSQLTable2);
    end;
    myADO2.Free;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
CreateSQLTable_2;
end;

procedure TForm1.InsertSQLTable(nameprn,modelprn,ipprn,snprn,prnpages,prncolorpages,dateprn,timeprn,datetimeprn:string);
  var
   myADO : TADOQuery;
Begin
 myADO:=TADOQuery.Create(Form1);
    myADO.ConnectionString:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog='+NameSQLBase+';'+
    'Data Source='+NameSQLServer;
    myADO.SQL.Clear;
    myADO.SQL.Text:='Insert INTO '+NameSQLTable+' (Имя_принтера, Модель_принтера, IP, Серийный_номер, Количество_страниц, Количество_цветных_страниц, Дата_проверки, Время_проверки, ДатаВремя_проверки) '+
    'VALUES (:Имя_принтера, :Модель_принтера, :IP, :Серийный_номер,:Количество_страниц, :Количество_цветных_страниц, :Дата_проверки,:Время_проверки, :ДатаВремя_проверки)';
    myADO.Parameters.ParamByName('Имя_принтера').Value := nameprn;
    myADO.Parameters.ParamByName('Модель_принтера').Value := modelprn;
    myADO.Parameters.ParamByName('IP').Value := ipprn;
    myADO.Parameters.ParamByName('Серийный_номер').Value := snprn;
    try
      myADO.Parameters.ParamByName('Количество_страниц').Value := prnpages;
    except

      // ShowMessage('Ошибка в количестве страниц! '+prnpages+'; Принтер: '+nameprn+' IP: '+ipprn);
    end;
    myADO.Parameters.ParamByName('Количество_цветных_страниц').Value := '0';
    myADO.Parameters.ParamByName('Дата_проверки').Value := StrToDateTime(dateprn);
    myADO.Parameters.ParamByName('Время_проверки').Value := StrToDateTime(timeprn);
    myADO.Parameters.ParamByName('ДатаВремя_проверки').Value := StrToDateTime(datetimeprn);
    try
        myADO.ExecSQL;
    except
        //ShowMessage('Ошибка добавления в '+NameSQLTable);
    end;
    myADO.Free;
end;

procedure DeleteSQLTable_2(ipprn:string);
  var
   myADO : TADOQuery;
   s:string;
Begin
 myADO:=TADOQuery.Create(Form1);

    myADO.ConnectionString:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog=prnBase;'+
    'Data Source='+NameSQLServer;
    myADO.SQL.Clear;
    myADO.SQL.Text:='DELETE FROM '+NameSQLTable2+' WHERE [Имя_принтера] =:Имя_принтера;';
    myADO.Parameters.ParamByName('Имя_принтера').Value := ipprn;
    try
        myADO.ExecSQL;
    except
        ShowMessage('Ошибка с удаления');
    end;
    myADO.Free;
end;


procedure TForm1.Button5Click(Sender: TObject);
begin
CreateSQLTable_Model;
end;

procedure TForm1.Button6Click(Sender: TObject);
 var
     sp1,sp2:TStringList;
     i,j:integer;
     R : array of Cardinal;
     res:boolean;
     AvgMS:double;
     ATimes:byte;
     model,prn_hostname,mydate,mytime:string;
begin
  sp1:=TStringList.Create;
  sp2:=TStringList.Create;
  sp1.LoadFromFile(dir_app+'\list1.txt');
  sp2.LoadFromFile(dir_app+'\list4.txt');
  For i:=0 to sp2.Count-1 do
    begin
        Res := True;
        AvgMS := 0;
        ATimes:=2;
        if ATimes>0 then
          with TIdIcmpClient.Create(Form1) do
          try
              Host := sp2[i];
              ReceiveTimeout:=500; //TimeOut du ping
              SetLength(R,ATimes);
              {Pinguer le client}
              for j:=0 to Pred(ATimes) do
              begin
                  try
                    Ping();
                    Application.ProcessMessages; //ne bloque pas l'application
                    R[j] := ReplyStatus.MsRoundTripTime;
                  except
                    Res := False;
                  end;
                if ReplyStatus.ReplyStatusType<>rsEcho Then res := False; //pas d'e'cho, on renvoi false.
              end;
              {Faire une moyenne}
              for j:=Low(R) to High(R) do
              begin
                Application.ProcessMessages;
                AvgMS := AvgMS + R[j];
              end;
              AvgMS := AvgMS / j;
          finally
              Free;
          end;
          if res then
           begin
               prn_hostname:=sp1[i];
               mydate:=DateToStr(now);
               mytime:=TimeToStr(now);
               if case_mb(model) then
                  prn_hostname:=SNMPQuery(sp2[i],hostname);
               InsertSQLTable_2(i,prn_hostname,'unknown',sp2[i],model,mydate,mytime);
           end ;
    end;
  sp1.Free;
  sp2.Free;
  ShowMessage('Готово')
end;

Procedure get_ip_prn;
  var
   myADO : TADOQuery;
   s,s1:string;

Begin
    myADO:=TADOQuery.Create(Form1);
    s1:='Provider=SQLOLEDB.1;'+
    'Integrated Security=SSPI;'+
    'Persist Security Info=False;'+
    'Initial Catalog=prnBase;'+
    'Data Source='+NameSQLServer;
    myADO.ConnectionString:=s1;
    myADO.SQL.Clear;
    myADO.SQL.Text:='Select * FROM '+NameSQLTable2;
    PrnList.Clear;
    try
        myADO.Open;
     while not myADO.EOF do
        begin
          s := myADO.Fields[3].AsString ;
          PrnList.Append(Trim(s));
          myADO.Next;
        end;
    except
       // ShowMessage('Ошибка получения списка IP принтеров');
       Form1.Memo1.Lines.Append(DateToStr(now) + ' ' + TimeToStr(now)+ ' Ошибка получения списка IP принтеров!');

    end;
    Form1.Memo1.Lines.Append(DateToStr(now) + ' ' + TimeToStr(now)+ ' Получен список IP принтеров!');
    MyADO.Close;
    myADO.Free;

End;


Procedure Load_setting;
 var
  f:textfile;
  s:string;
Begin
 if fileexists(dir_app+'\Setting.ini') then
   begin
      assignfile(f, dir_app+'\Setting.ini');
      reset(f);
      readln(f,s);
      Form1.Edit3.Text:=copy(s,Pos('=',s)+1,Length(s)-Pos('=',s));
      readln(f,s);
      Form1.Edit1.Text:=copy(s,Pos('=',s)+1,Length(s)-Pos('=',s));
      readln(f,s);
      s:=copy(s,Pos('=',s)+1,Length(s)-Pos('=',s));
      if s='true' then Form1.CheckBox1.Checked:=true;
      readln(f,s);
      s:=copy(s,Pos('=',s)+1,Length(s)-Pos('=',s));
      if s='true' then Form1.CheckBox2.Checked:=true;
      closefile(f);
    end
     else
    begin
      Form1.Edit1.Text:='';
    end;
End;

procedure TForm1.FormShow(Sender: TObject);
begin
PrnList:=TStringList.Create;
Load_setting;
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
try
PrnList.Free;
except

end;
Application.Terminate;
end;


procedure TForm1.Button7Click(Sender: TObject);
begin
if Form1.Edit1.Text='' then  exit;


NameSQLServer:=Form1.Edit3.Text;
if Form1.Button7.Caption='Запустить' then
  begin
    Form1.Button7.Caption:='Остановить';
    Form1.Timer1.Interval:=999;
    Form1.Timer1.Enabled:=true;
    b_time:=Form1.CheckBox1.Checked;
    b_date:=Form1.CheckBox2.Checked;
    Form1.Memo1.Lines.Append(DateToStr(now) + ' ' + TimeToStr(now)+ ' Сервер запущен!');
  end
   else
  begin
    Form1.Button7.Caption:='Запустить';
    Form1.Timer1.Enabled:=false;
    NewThread.Terminate;
    Form1.Memo1.Lines.Append(DateToStr(now) + ' ' + TimeToStr(now)+ ' Сервер остановлен!');
  end;

end;

function DayOfWeekRus(S: TDateTime): string;
const 
  week:array[1..7] of string = ('Воскресенье','Понедельние','Вторник','Среда','Четверг','Пятница','Суббота');
var
  day:integer;
begin
day:=DayOfWeek(s);
Result:=week[day];
end;

procedure TForm1.Timer1Timer(Sender: TObject);
 var
  b,b2:boolean;
begin
  b:=false;
  if b_time then
   if (TimeToStr(now)>'07:00:00') and (TimeToStr(now)<'19:00:00') then b:=true;

  b2:=false;
  if b_date then
   if (DayOfWeekRus(now)='Суббота') or (DayOfWeekRus(now)='Воскресенье') then b2:=true;

  if (not b) and (not b2) then
     begin
        Form1.Memo1.Lines.Append(DateToStr(now) + ' ' + TimeToStr(now)+ ' обновление ...!');

        if Form1.Timer1.Interval=999 then Form1.Timer1.Interval:=StrToInt(Form1.Edit1.Text)*60000;
        get_ip_prn;
        if NewThread<>nil then NewThread.Terminate;
        NewThread:=TNewThread.Create(true);
        NewThread.AHost:='';
        NewThread.ATimes:=2;
        NewThread.AvgMS:=500;
        NewThread.List:=PrnList;
        NewThread.FreeOnTerminate:=true;
        NewThread.Priority:=tpNormal;
        NewThread.Resume;
   end;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
CreateSQLTable_group;
end;

procedure TForm1.N2Click(Sender: TObject);
begin
Close;
end;

procedure TForm1.N1Click(Sender: TObject);
begin
if Form1.PopupMenu1.Items[0].Caption='Свернуть' then
  begin
      ShowWindow(Form1.Handle,SW_HIDE);
      ShowWindow(Application.Handle, SW_HIDE);
      Application.ShowMainForm := False;
      Application.MainForm.Hide;
      Form1.PopupMenu1.Items[0].Caption:='Развернуть';
  end
   else
  begin
      ShowWindow(Form1.Handle,SW_Show);
      ShowWindow(Application.Handle, SW_show);
      Application.ShowMainForm := true;
      Application.MainForm.Show;
      Form1.PopupMenu1.Items[0].Caption:='Свернуть';
  end;


end;

procedure TForm1.Button8Click(Sender: TObject);
begin
CreateSQLTable_Profile;
end;

end.

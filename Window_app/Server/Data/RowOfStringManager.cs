
using SolarServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Data
{
    class RowOfStringManager
    {
        public bool IsConfiged { set; get; }
        private static RowOfStringManager instance;
        private List<RowOfPanel> listRowOfPanel;
        public static object _lockWriteObject = new object();
        public List<RowOfPanel> RowOfPanelList
        {
            get
            {
                return listRowOfPanel;
            }
        }
        private RowOfStringManager()
        {
            listRowOfPanel = new List<RowOfPanel>();
           // CreateListSolarPanel();
        }
        private static object _lockObject = new object();
        public static RowOfStringManager Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (instance == null)
                    {
                        instance = new RowOfStringManager();
                    }
                    return instance;
                }
            }
        }
        public void Clear()
        {
            listRowOfPanel.Clear();
            IsConfiged = false;
        }
        private void CreateListSolarPanel()
        {
            List<SolarPanel> solarPanels = new List<SolarPanel>();
            solarPanels.Add(new SolarPanel() { Id = "1", isHasRobot = true, IdString = "1", IdRobot = "3" });
            solarPanels.Add(new SolarPanel() { Id = "2", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "3", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "4", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "5", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "6", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "7", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "8", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "9", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "10", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "11", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "12", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "13", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "14", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "15", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "16", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "17", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "18", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "19", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "20", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "21", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "22", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "23", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "24", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "25", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "26", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "27", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "28", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "29", isHasRobot = false, IdString = "1" });
            solarPanels.Add(new SolarPanel() { Id = "30", isHasRobot = false, IdString = "1" });

            List<SolarPanel> solarPanels_1 = new List<SolarPanel>();
            solarPanels_1.Add(new SolarPanel() { Id = "1", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "2", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "3", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "4", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "5", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "6", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "7", isHasRobot = true, IdString = "1", IdRobot = "2" });
            solarPanels_1.Add(new SolarPanel() { Id = "8", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "9", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "10", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "11", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "12", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "13", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "14", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "15", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "16", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "17", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "18", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "19", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "20", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "21", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "22", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "23", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "24", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "25", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "26", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "27", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "28", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "29", isHasRobot = false, IdString = "1" });
            solarPanels_1.Add(new SolarPanel() { Id = "30", isHasRobot = false, IdString = "1" });

            List<SolarPanel> solarPanels_2 = new List<SolarPanel>();
            solarPanels_2.Add(new SolarPanel() { Id = "1", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "2", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "3", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "4", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "5", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "6", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "7", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "8", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "9", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "10", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "11", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "12", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "13", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "14", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "15", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "16", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "17", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "18", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "19", isHasRobot = true, IdString = "1", IdRobot = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "20", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "21", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "22", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "23", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "24", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "25", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "26", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "27", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "28", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "29", isHasRobot = false, IdString = "1" });
            solarPanels_2.Add(new SolarPanel() { Id = "30", isHasRobot = false, IdString = "1" });


            listRowOfPanel.Add(new RowOfPanel() { Id = "1", nameString = "String 1", solarPanels = solarPanels });
            listRowOfPanel.Add(new RowOfPanel() { Id = "2", nameString = "String 2", solarPanels = solarPanels_1 });
            listRowOfPanel.Add(new RowOfPanel() { Id = "3", nameString = "String 3", solarPanels = solarPanels_2 });
            listRowOfPanel.Add(new RowOfPanel() { Id = "4", nameString = "String 4", solarPanels = solarPanels_2 });
            listRowOfPanel.Add(new RowOfPanel() { Id = "5", nameString = "String 5", solarPanels = solarPanels_1 });
            listRowOfPanel.Add(new RowOfPanel() { Id = "6", nameString = "String 6", solarPanels = solarPanels_2 });
            listRowOfPanel.Add(new RowOfPanel() { Id = "7", nameString = "String 7", solarPanels = solarPanels });
            listRowOfPanel.Add(new RowOfPanel() { Id = "8", nameString = "String 8", solarPanels = solarPanels_1 });
            listRowOfPanel.Add(new RowOfPanel() { Id = "9", nameString = "String 9", solarPanels = solarPanels_1 });
            listRowOfPanel.Add(new RowOfPanel() { Id = "10", nameString = "String 10", solarPanels = solarPanels_1 });
            listRowOfPanel.Add(new RowOfPanel() { Id = "11", nameString = "String 11", solarPanels = solarPanels_1 });
            
        }
        public void UpdatePostion(SolarMachine solarMachine)
        {
            lock (_lockObject)
            {
                var panel = new SolarPanel();
                panel.isCurrentPositionOfMachine = true;
                panel.isHasRobot = true;
                panel.IdString = solarMachine.Position.Y;
                panel.IdRobot = solarMachine.IDRobot;
                panel.position = solarMachine.Position.X;


                for (int i=0;i<listRowOfPanel.Count;i++)
                {
                    try
                    {
                        var item = listRowOfPanel.ElementAt(i);
                        if (item.solarPanels!=null&&item.solarPanels.Count>0)
                        {
                            var existPanel = item.solarPanels.FirstOrDefault(p => p.isHasRobot == true && p.IdRobot == solarMachine.IDRobot);
                            if (existPanel!=null)
                            {
                                existPanel.IdRobot = "";
                                existPanel.isHasRobot = false;
                            }
                           
                        }
                    }catch
                    {

                    }
                }
                var row = listRowOfPanel.FirstOrDefault(i => i.Id == solarMachine.Position.Y);
                if (row!=null)
                {
                    var existPanel = row.solarPanels.FirstOrDefault(p => p.position == solarMachine.Position.X);
                    if (existPanel == null)
                    {
                        row.solarPanels.Add(panel);
                        row.solarPanels.Sort((x,y) => int.Parse(x.position).CompareTo(int.Parse(y.position)));
                    } else
                    {
                        existPanel.isHasRobot = true;
                        existPanel.IdString = solarMachine.Position.Y;
                        existPanel.IdRobot = solarMachine.IDRobot;
                        existPanel.position = solarMachine.Position.X;
                    }
                    SettingManager.Instance().SavePlantMap();
                } else
                {
                    row = new RowOfPanel();
                    row.solarPanels.Add(panel);
                    row.Id = solarMachine.Position.Y;
                    listRowOfPanel.Add(row);
                    SettingManager.Instance().SavePlantMap();
                }
            }
           
           
        }
        public void InitFistList(List<RowOfPanel> rowOfPanels)
        {
            IsConfiged = true;
            RowOfPanelList.Clear();
            listRowOfPanel = rowOfPanels;
        }
    }
}

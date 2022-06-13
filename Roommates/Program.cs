using System;
using System.Collections.Generic;
using System.Linq;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }
                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();
                        Console.Write("New Max Occupancy");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete a room"):
                        List<Room> roomList = roomRepo.GetAll();
                        foreach (Room r in roomList)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }
                        Console.WriteLine("Select a room to delete");
                        int roomToDelete = int.Parse(Console.ReadLine());
                        roomRepo.Delete(roomToDelete);
                        Console.WriteLine($"Room {roomToDelete} has been deleted");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName,
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Update Chore"):
                        List<Chore> choreOptions = choreRepo.GetAll();
                        foreach (Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.WriteLine("Which chore would you like to update? ");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreOptions.FirstOrDefault(c => c.Id == selectedChoreId);

                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();
                        
                        choreRepo.Update(selectedChore);

                        Console.WriteLine("Chore has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete a chore"):
                        List<Chore> choreList = choreRepo.GetAll();
                        foreach (Chore c in choreList)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.WriteLine("Select a chore to delete");
                        int choreToDelete = int.Parse(Console.ReadLine());
                        choreRepo.Delete(choreToDelete);
                        Console.WriteLine($"Chore {choreToDelete} has been deleted");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Get Unassigned Chores"):
                        List<Chore> unChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unChores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Assign chore to roommate"):
                        List<Chore> allChores = choreRepo.GetAll();
                        foreach (Chore c in allChores)
                        {
                            Console.WriteLine($"{c.Id}. {c.Name}");
                        }
                        Console.WriteLine("Select a chore: ");
                        int choreChoice = int.Parse(Console.ReadLine());
                        List<Roommate> allRoommates = roommateRepo.GetAll();
                        foreach (Roommate rm in allRoommates)
                        {
                            Console.WriteLine($"{rm.Id}. {rm.FirstName}");
                        }
                        int roommateChoice = int.Parse(Console.ReadLine());
                        choreRepo.AssignChore(roommateChoice, choreChoice);
                        Console.WriteLine("Assigned");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show chore assignment count"):
                        Dictionary<string, int> choreCount = choreRepo.GetChoreCounts();
                        foreach (KeyValuePair<string, int> c in choreCount)
                        {
                            Console.WriteLine($"{c.Key}: {c.Value}");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search Roommate by Id"):
                        Console.Write("Roommate Id: ");
                        int roommateId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(roommateId);

                        Console.WriteLine($"{roommate.FirstName} - {roommate.RentPortion}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Reassign Chore"):
                        List<Chore> getChores = choreRepo.GetAllAssigned();
                        foreach (Chore c in getChores)
                        {
                            Console.WriteLine($"{c.Id}- {c.Name}");
                        }
                        int choosenChoreId = int.Parse(Console.ReadLine());
                        List<Roommate> assignedRoomates = roommateRepo.GetRoommatesByChoreId(choosenChoreId);
                        Console.WriteLine("This chore is currently assigned to: ");
                        foreach (Roommate r in assignedRoomates)
                        {
                            Console.WriteLine($"{r.Id}- {r.FirstName}");
                        }
                        Console.WriteLine("Who would you like to assign it to?");
                        List<Roommate> getAllRoommates = roommateRepo.GetAll();
                        foreach (Roommate r in getAllRoommates)
                        {
                            Console.WriteLine($"{r.Id}- {r.FirstName}");
                        }
                        int choosenRoommateId = int.Parse(Console.ReadLine());
                        roommateRepo.UpdateRoommateChore(choosenRoommateId, choosenChoreId);
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Delete a room",
                "Show all chores",
                "Search for chore",
                "Add a chore",
                "Update Chore",
                "Delete a chore",
                "Get Unassigned Chores",
                "Assign chore to roommate",
                "Reassign Chore",
                "Show chore assignment count",
                "Search Roommate by Id",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}

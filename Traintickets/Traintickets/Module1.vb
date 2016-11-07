Imports System.IO ' this is for a random number generator later on
'I know this is definetly not a very efficient way of doing this, but it works so meh.
' Also, you might have to hit enter again after pressing r to do a return ticket. god knows why.

Module Module1
    Dim rightNow As DateTime = DateTime.Now
    Dim Time As String ' declare time variables
    Dim offpeak As Boolean

    Sub Main() ' time related stuff
        Console.TreatControlCAsInput = True ' Stops closing of program using ^C

        Console.Clear()
        Console.ForegroundColor = ConsoleColor.White
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.White
        Time = rightNow.ToString("H:mm")
        Console.WriteLine(Time)
        Dim hr As Integer
        Dim ctime As Date

        ctime = DateTime.Now
        hr = ctime.Hour

        If hr <= 7 Or hr >= 18 Then
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine("Currently Off-Peak")
            offpeak = True
            Console.ForegroundColor = ConsoleColor.White
        End If
        Ticket()

    End Sub
    Dim adult As String
    Dim child As String
    Dim station As String ' declare all the variables i need
    Dim dest As String
    Dim returnt As Boolean
    Dim inputt As String
    Dim lines() As String = {"1. Walsall",
"2. Bermingham New Street",
"3. Bermingham International",
"4. Coventry",
"5. Rugby",
"6. Northampton",
"7. Milton Keynes",
"8. Watford",
"9. London Euston"}     ' Using an array here so i don't have to 'console.writeline()' 7000 times
    Sub Ticket() ' sub to get ticket info
aticket:
        Console.ForegroundColor = ConsoleColor.Blue
        Console.WriteLine("How many adult tickets would you like?")
        adult = Console.ReadLine()
        If IsNumeric(adult) AndAlso (adult >= 0 And adult <= 50) Then
            GoTo cticket
        Else
            GoTo aticket
        End If
cticket:
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("How many child tickets would you like?") ' get ticket infos
        child = Console.ReadLine()
        If IsNumeric(child) AndAlso (child >= 0 And child <= 50) Then
            GoTo choosestation
        Else
            GoTo cticket
        End If
choosestation:
        Console.ForegroundColor = ConsoleColor.Magenta

        Console.WriteLine("What train station are you currently at?")
        For Each line As String In lines
            Console.WriteLine(line) ' print off list one line by one
        Next
        station = Console.ReadLine()
        If IsNumeric(station) AndAlso (station > 0 And station < 10) Then ' check if it is between 1 and 9 and if its a number 
            GoTo choosedest
        Else
            GoTo choosestation ' YES I KNOW GOTOS ARE INEFFICIANT AND STUFF, THEY WORK M'KAY?
        End If

choosedest:
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("What train station are you travelling to?")
        For Each line As String In lines
            Console.WriteLine(line) ' print the array line by line
        Next

        dest = Console.ReadLine()
        If IsNumeric(dest) AndAlso (dest > 0 And dest < 10) Then
            GoTo destcheck
        Else
            GoTo choosedest
        End If

destcheck:
        If dest = station Then
            GoTo choosestation
            Console.ForegroundColor = ConsoleColor.Red

            Console.WriteLine("Destination can not be the same as Start.") ' check for incorrect entries
        End If
returnticket:
        Console.ForegroundColor = ConsoleColor.Blue
        Console.WriteLine("What kind of ticket do you require? S/R")
        Console.WriteLine("Single (S)") ' get singular or return ticket
        Console.WriteLine("Return (R)")
        inputt = Console.ReadLine()
        If inputt = "s" Or inputt = "S" Then
            returnt = False
            calc()
        ElseIf inputt = "r" Or inputt = "R" Then 'get return or single 
            returnt = True
            calc()
        Else
            GoTo returnticket
        End If


    End Sub
    Dim less5 As Boolean = False
    Dim less7 As Boolean = False
    Dim more7 As Boolean = False 'more variables
    Dim final As Decimal
    Dim work As Decimal = 1.85 ' delcare decimals 
    Dim cwork As Decimal = 1.18
    Dim value As String = GetRandomString() ' used for rng later on
    Sub calc()


        If dest - station <= 5 Then
            less5 = True
        ElseIf dest - station <= 7 Then ' most of the next code is copypaste so ill explain it next
            less7 = True
        ElseIf dest - station > 7 Then
            more7 = True
        End If

        If less5 = True Then ' if its less than 5 stations
            If offpeak = True Then ' if its off peak
                work = work - (work * 0.25) ' -25% off of work
            End If
            If returnt = True Then ' if its return ( returnt because it doesn't like me using return ) 
                work = work - (work * 0.13) ' -13% off of work
            End If
            work = work * (dest - station) ' times the price so it's per-stop
        End If

        If less7 = True Then
            work = 1.85
            If offpeak = True Then
                work = work - (work * 0.25)
            End If
            If returnt = True Then
                work = work - (work * 0.13)
            End If
            work = work * (dest - station)
        End If
        If more7 = True Then
            work = 1.85
            If offpeak = True Then
                work = work - (work * 0.25)
            End If
            If returnt = True Then
                work = work - (work * 0.13)
            End If
            work = work * (dest - station)
        End If
        work = work * adult ' times the ticket value by the amount of adult tickets

        cwork = 1.18
        If less5 = True Then
            If offpeak = True Then
                work = work - (work * 0.25)
            End If
            If returnt = True Then
                work = work - (work * 0.13)
            End If
            work = work * (dest - station)
        End If

        If less7 = True Then
            cwork = 1.18
            If offpeak = True Then
                work = work - (work * 0.25)
            End If
            If returnt = True Then
                work = work - (work * 0.13)
            End If
            work = work * (dest - station)
        End If

        If more7 = True Then
            cwork = 1.18
            If offpeak = True Then
                work = work - (work * 0.25)
            End If
            If returnt = True Then
                work = work - (work * 0.13)
            End If
            work = work * (dest - station)
        End If
        cwork = cwork * child ' times child value by amount of tickets

        final = cwork + work ' add both adult and child values


        final = Decimal.Round(final, 2, MidpointRounding.AwayFromZero) ' round to nearest 2 decimals (probably not the easiest way of doing it, thanks stackoverflow)
        Console.ForegroundColor = ConsoleColor.Cyan ' set to cyan
        Console.WriteLine("******* FINAL PRICING *******")
        Console.WriteLine("Ticket Cost: £" & final) ' print cost
        Console.WriteLine("Ticket Code: " & value)

        Threading.Thread.Sleep(5000)
        Console.WriteLine("Press enter to continue...")

        Console.Read()
        Console.Clear()
        Main()

    End Sub

    Public Function GetRandomString() ' generates random string for ticket cost
        Dim p As String = Path.GetRandomFileName() ' uses system.io magic to do stuff with filenames to get stuff
        p = p.Replace(".", "")
        Return p
    End Function


End Module

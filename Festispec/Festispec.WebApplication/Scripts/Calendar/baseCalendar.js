today = new Date();
currentMonth = today.getMonth();
currentYear = today.getFullYear();
selectedYear = document.getElementById("year");
selectedMonth = document.getElementById("month");

months = ["Jan", "Feb", "Mrt", "Apr", "Mei", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"];

monthAndYear = document.getElementById("monthAndYear");
showCalendar(currentMonth, currentYear);

// Functie voor het maken van de kalender
function showCalendar(month, year)
{

    let firstDay = (new Date(year, month)).getDay();

    // Body van de kalender
    tbl = document.getElementById("calendar-body");

    // Leegmaken van alle vakjes
    tbl.innerHTML = "";

    // Invullen van de maand en het jaar via DOM
    monthAndYear.innerHTML = months[month] + "/" + year;
    selectedMonth.value = month;
    selectedYear.value = year;

    // Alle 42 vakjes maken
    let date = 1;
    for (let x = 0; x < 6; x++)
    {
        // voegt een rij toe
        let row = document.createElement("tr");

        // alle vakjes maken en vullen met de juiste gegevens
        for (let y = 0; y < 7; y++)
        {
            if (x === 0 && y < firstDay) {
                cell = document.createElement("td");
                cellText = document.createTextNode("");
                cell.appendChild(cellText);
                row.appendChild(cell);
            }
            else if (date > daysInMonth(month, year))
                break;

            else
            {
                cell = document.createElement("td");
                cellText = document.createTextNode(date);
                // De dag van vandaag een andere kleur geven
                if (date === today.getDate() && year === today.getFullYear() && month === today.getMonth())
                    cell.classList.add("bg-info");

                cell.appendChild(cellText);
                row.appendChild(cell);
                date++   
            }  
        }

        // Elke rij toevoegen aan de body
        tbl.appendChild(row);
    }
}

// functie om naar de volgende maand te springen
function next()
{
    // Als currentMonth 11 (dec) is dan gaat currentYear met 1 omhoog, zo niet dan blijft het hetzelfde
    currentYear = (currentMonth === 11) ? currentYear + 1 : currentYear;
    currentMonth = (currentMonth + 1) % 12;
    showCalendar(currentMonth, currentYear);
}

function previous()
{
    // Als currentMonth 0 (jan) is dan gaat currentYear met 1 omlaag, zo niet dan blijft het hetzelfde
    currentYear = (currentMonth === 0) ? currentYear - 1 : currentYear;
    currentMonth = (currentMonth === 0) ? 11 : currentMonth - 1;
    showCalendar(currentMonth, currentYear);
}

function jump()
{
    currentYear = parseInt(selectedYear.value);
    currentMonth = parseInt(selectedMonth.value);
    showCalendar(currentMonth, currentYear);
}

// https://dzone.com/articles/determining-number-days-month wordt hier uitgelegd
function daysInMonth(iMonth, iYear)
{
    return 32 - new Date(iYear, iMonth, 32).getDate();
}


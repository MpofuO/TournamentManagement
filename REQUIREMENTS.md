# Football Tournament Manager: Requirements

Version 1.0 (final). Build window: 27 October – 15 November 2026.

## 1. Purpose

A tool for one organiser to run soccer league tournaments: keep a shared list of teams, create round-robin tournaments from them, schedule matches by date, record results, and see live standings.

This document describes what the application does and what the person using it sees. It deliberately says nothing about how it is built.

## 2. Users

**The organiser** is the only user. There are no logins, no player or spectator views, and no other roles. The organiser can do everything described below.

The application must be comfortable to use on a laptop screen. Phone use is not required.

## 3. Key rules at a glance

- **Format:** single round-robin. Every team plays every other team once.
- **Points:** win 3, draw 1, loss 0.
- **Team names** are unique across the whole application. Capital letters and extra spaces are ignored when comparing names. A team has a name only.
- **Team count:** 4 to 16 teams per tournament.
- **One match per team per day,** across all tournaments.
- **Name length:** team and tournament names are 1 to 40 characters.
- **Scores:** whole numbers from 0 to 99.

## 4. Use cases

1. **Manage teams.**
   1. Open the Teams screen.
   2. Add one team by name, or paste a list of names (one per line). The app shows which names already exist and which are new, and the organiser confirms.
   3. A team can be renamed. The new name applies everywhere the team appears.
   4. A team can be deleted only if it is in no tournament.

2. **Create a tournament.**
   1. On the tournaments list, click Create tournament.
   2. Enter a unique name.
   3. The tournament starts with the status *Setting up*.

3. **Add teams to a tournament.**
   1. Open a tournament in *Setting up* and go to its Teams section.
   2. Pick teams from the shared team list.
   3. Alternatively, choose "add top N from a finished tournament". Pick the tournament and N. The app pre-ticks that tournament's top N teams, and the organiser can untick any of them or add others before confirming.
   4. Teams can be removed freely while the tournament is in setup.

4. **Start a tournament.**
   1. With at least 4 teams entered, click Start.
   2. The app creates every match, grouped into rounds.
   3. Teams can no longer be added or removed (withdrawing is covered in use case 9).
   4. The status becomes *In progress*.

5. **Set a match date.**
   1. Open a match and pick a day, or clear the date.
   2. If either team already has another match on that day, in any tournament, the app refuses. The message names the team, the day and the other match.

6. **Enter a result.**
   1. Open a match.
   2. Enter the goals for each team.
   3. Save. The match becomes *Played* and the standings update immediately.

7. **Correct a result.**
   1. Open a played match.
   2. Change the score, or remove the result so the match returns to *Upcoming*.
   3. The standings update immediately. This works at any time, including after the tournament is finished.

8. **View standings and fixtures.** Open a tournament to see the table, the fixtures by round, the most recent results and the next matches.

9. **Withdraw a team.**
   1. In an in-progress tournament, open the Teams section and choose Withdraw for a team.
   2. Confirm. This is final.
   3. See section 6 for what happens to its matches and its place in the table.

10. **Rename or delete a tournament.**
    1. Rename at any time.
    2. Delete after a confirmation. Its matches and results are lost, but its teams stay in the shared list.

11. **Example tournament.**
    1. An 8-team example tournament with some results already entered is present when the application is first opened.
    2. A button on the tournaments list adds it again after it has been deleted.

## 5. Screens

### A. Tournaments list (first screen)

- One row per tournament: name, status (*Setting up*, *In progress* or *Finished*), number of teams, and matches played out of total.
- Buttons: Create tournament, Add example tournament.
- Clicking a row opens the tournament.

### B. Teams

- A list of all teams. Each team shows the tournaments it is in.
- Actions: add a team, paste a list of names, rename, delete.

### C. Tournament

The header shows the tournament name and status, with rename and delete actions. There are three sections.

**Standings**
- One row per team: position, name, played, won, drawn, lost, goals for, goals against, goal difference, points, and recent form.
- Recent form shows the last five played matches in round order as W, D or L.
- Beside the table: the five most recent results and the next five upcoming matches. Upcoming matches are listed in date order, with undated ones after them in round order.

**Fixtures**
- Matches grouped by round. Each match shows both teams, the date (if set), and the score or "Upcoming".
- Actions on a match: set date, enter result, correct result, remove result.
- In a round with an odd number of teams, the resting team is shown.

**Teams**
- While *Setting up*: the team picker from use case 3, and the Start button.
- While *In progress*: the entered teams, each with a Withdraw action.

Match dates and results are entered in a small pop-up on a match, not on a separate screen.

## 6. Rules and awkward situations

| Situation | What happens |
| --- | --- |
| Odd number of teams | Each round, one team rests. Every team rests the same number of times. A rest is shown but not scored. |
| Fewer than 4 teams at start | Start is unavailable and explains why. |
| More than 16 teams | A seventeenth team cannot be added. The app explains the limit. |
| Teams tied on points | Ranked by goal difference, then by goals scored. If still level, they share the same position and are listed alphabetically. |
| Result entered wrongly | Corrected or removed as in use case 7. |
| Date breaks the one-match-a-day rule | Refused, as in use case 5. Matches without a date are not checked. Cancelled matches do not count. |
| Team withdraws | Final, after a confirmation. Results already played stand. Its remaining matches are cancelled and removed from upcoming matches. It stays in the table, marked *Withdrawn*, below all other teams. |
| All matches played or cancelled | The tournament becomes *Finished* by itself. Results can still be corrected. |
| Team deleted while in a tournament | Refused. The message names the tournaments it is in. |
| Duplicate team name | Refused. In a pasted list, an existing name uses the existing team and the new names are created. |
| Duplicate tournament name | Refused. |
| Empty or over-long name | Refused (names must be 1 to 40 characters). |
| Score that is not a whole number from 0 to 99 | Refused. |
| Same team in several unfinished tournaments | Allowed. Each tournament keeps its own record for the team. |
| Deleting a tournament | Confirmation first. Teams are kept. |
| Top-N shortcut with ties at the cut-off | The tied teams are all shown. The organiser decides by ticking or unticking. |

## 7. Scope

### Included in the first version

Everything in sections 3 to 6.

### First to cut if time runs out

1. The "add top N from a finished tournament" shortcut. Picking teams by hand from the list still works.
2. Match dates and the one-match-a-day rule. Matches would be grouped by round only.

### Later

- Kick-off times (so the clash rule works on overlapping times, not just days)
- An app-suggested calendar for a tournament
- Head-to-head tie-breaking
- Undoing a withdrawal
- A history of corrected results
- Other formats: knockout, home-and-away, group stages
- A read-only view for spectators
- A simulated demo mode
- Importing teams or results from a spreadsheet
- A dark, card-style look inspired by the reference screenshot
- A season picker
- Hover pop-ups showing the last result

### Not included

- Venues or pitches
- Squads and goal scorers
- Logos or team profiles
- Entry fees and rewards
- Logins and multiple organisers
- Search
- All-time statistics across tournaments

## 8. First version is done when

- [ ] I can add, paste, rename and delete teams, and duplicate names are caught.
- [ ] I can create a tournament and pick its teams.
- [ ] Starting creates every match in rounds, including with an odd number of teams.
- [ ] I can set a match date, and a clashing date is refused with a clear message.
- [ ] I can enter, correct and remove a result, and the table updates immediately.
- [ ] The standings show all columns, ranked correctly, with ties handled as described.
- [ ] A withdrawn team behaves as described.
- [ ] A tournament becomes *Finished* by itself when all its matches are played or cancelled.
- [ ] The same team can be in two tournaments, and each tournament has its own record for it.
- [ ] The example tournament is available on first opening and can be added again.
- [ ] Everything above is comfortable to use on a laptop screen.

## 9. Open questions

None for the first version. Decisions made in the last round:

- Played results stand when a team withdraws.
- The limit is 16 teams per tournament.
- A laptop screen is enough; phone support is not required.

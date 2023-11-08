import ClockDisplay from './ClockDisplay'
import TasksDisplay from './TasksDisplay'

import './Dashboard.css'

export default function Dashboard() {
  return (
    <div className="dashboard-container">
      <ClockDisplay />
      <TasksDisplay />
    </div>
  )
}

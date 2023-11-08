import { useEffect, useState } from 'react'
import Clock from 'react-clock'
import moment from 'moment'

import 'react-clock/dist/Clock.css'
import './ClockDisplay.css'

export default function ClockDisplay() {
  const [value, setValue] = useState(moment())

  useEffect(
    () => {
      const interval = setInterval(() => setValue(moment()), 1000)

      return () => {
        clearInterval(interval)
      }
    },
    []
  )

  return (
    <div className="clock-display">
      <div className="clock-display__header">
        {value.format("dddd, MMMM Do YYYY, HH:mm")}
      </div>
      <Clock
        size="30vw"
        renderSecondHand={false}
        renderNumbers={true}
        value={value.format("HH:mm")}
        className="clock-display__analog"
      />
    </div>
  )
}

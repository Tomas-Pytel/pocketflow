interface RadialProgressProps {
  value?: number;
  strokeWidth?: number;
  label?: string;
  className?: string;
  color?: string;
  children?: React.ReactNode;
}

export default function RadialProgress({
  value = 0,
  strokeWidth = 10,
  label,
  className = "",
  color,
  children,
}: RadialProgressProps) {
  const size = 100; // viewBox units, not pixels
  const radius = (size - strokeWidth) / 2;
  const circumference = 2 * Math.PI * radius;
  const offset = circumference - (value / 100) * circumference;

  return (
    <div
      className={`relative flex items-center justify-center w-full h-full ${className}`}
    >
      <svg viewBox={`0 0 ${size} ${size}`} className="w-full h-full -rotate-90">
        <circle
          cx={size / 2}
          cy={size / 2}
          r={radius}
          strokeWidth={strokeWidth}
          className="fill-none stroke-muted"
        />
        <circle
          cx={size / 2}
          cy={size / 2}
          r={radius}
          strokeWidth={strokeWidth}
          strokeDasharray={circumference}
          strokeDashoffset={offset}
          strokeLinecap="round"
          style={color ? { stroke: color } : undefined}
          className="fill-none stroke-primary transition-all duration-500 ease-in-out"
        />
      </svg>
      <span className="absolute flex flex-col items-center justify-center gap-0.5">
        {/**<span className="text-sm font-bold text-foreground">{value}%</span> */}
        {children ? (
          children
        ) : (
          <span className="text-sm font-bold text-foreground">
            {label || `${value}%`}
          </span>
        )}
      </span>
    </div>
  );
}

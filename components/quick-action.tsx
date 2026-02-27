import { cn } from "@/lib/utils";

interface QuickActionProps {
  title: string;
  icon: React.ReactNode;
  handleClick?: () => void;
  className?: string;
  variant?: "mobile" | "desktop";
}

export default function QuickAction({
  title,
  icon,
  className,
  handleClick,
  variant = "desktop",
}: QuickActionProps) {
  return (
    <button
      className={cn(
        "flex w-full h-full items-center flex-col space-y-2 justify-center cursor-pointer",
        className,
      )}
      onClick={handleClick}
    >
      <div
        className={cn(
          "flex items-center justify-center bg-white dark:bg-gray-800 dark:border border-gray-700 rounded-xl p-3.5",
          variant === "mobile"
            ? "shadow  hover:scale-105"
            : "rounded-full bg-orange-200/50",
          "text-orange-500 hover:bg-orange-500 hover:text-white dark:hover:bg-orange-500",
        )}
      >
        {icon}
      </div>
      <span className="text-xs font-normal text-gray-500 dark:text-white mb-4">
        {title}
      </span>
    </button>
  );
}

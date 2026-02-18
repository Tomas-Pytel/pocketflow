interface QuickActionProps {
  title: string;
  icon: React.ReactNode;
  onClick?: () => void;
}

export default function QuickAction({ title, icon }: QuickActionProps) {
  return (
    <div className="flex w-full items-center flex-col space-y-2 cursor-pointer">
      <div className="flex items-center justify-center bg-white dark:bg-gray-800 dark:border border-gray-700 rounded-xl shadow p-3.5 text-orange-500 hover:bg-orange-500 hover:text-white hover:scale-105">
        {icon}
      </div>
      <span className="text-xs font-normal text-gray-500 mb-4">{title}</span>
    </div>
  );
}

interface TopNavigationProps {
  title?: string;
  userCard?: React.ReactNode;
  children?: React.ReactNode;
  className?: string;
}

export default function TopNavigation({
  children,
  title,
  className,
  userCard,
}: TopNavigationProps) {
  return (
    <nav
      className={`fixed top-0 left-0 right-0 bg-gray-100 dark:bg-gray-900 z-50 ${className || ""}`}
    >
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="w-full h-16 flex items-center justify-between ">
          {title && (
            <div className="text-xl font-bold text-orange-500">{title}</div>
          )}
          {userCard}

          <div className="flex items-center space-x-3 ml-auto flex-shrink-0">
            {children}
          </div>
        </div>
      </div>
    </nav>
  );
}

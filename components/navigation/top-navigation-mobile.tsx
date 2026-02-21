import { IoMdArrowBack } from "react-icons/io";

interface TopNavigationMobileProps {
  title?: string;
  userCard?: React.ReactNode;
  children?: React.ReactNode;
  className?: string;
  backButton?: boolean;
}

export default function TopNavigationMobile({
  children,
  title,
  className,
  userCard,
  backButton = false,
}: TopNavigationMobileProps) {
  return (
    <nav
      className={`fixed top-0 left-0 right-0 dark:bg-gray-900 z-50 ${className || ""} ${backButton ? "border-b border-gray-100 dark:border-gray-700 bg-white" : "bg-gray-50"}`}
    >
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="w-full h-16 flex items-center justify-between ">
          {backButton && <IoMdArrowBack className="h-5 w-5 text-orange-500 " />}
          {title && (
            <div className="text-xl font-bold text-gray-900 dark:text-gray-100">
              {title}
            </div>
          )}
          {userCard}

          <div
            className={`flex items-center space-x-3 ${backButton ? "" : "ml-auto"} flex-shrink-0`}
          >
            {children}
          </div>
        </div>
      </div>
    </nav>
  );
}

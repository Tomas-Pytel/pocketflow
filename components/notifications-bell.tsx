import { IoIosNotifications } from "react-icons/io";
export default function NotificationsBell() {
  return (
    <button className="flex bg-white dark:bg-gray-800 p-1 rounded-full text-gray-500 dark:text-gray-100 hover:text-gray-700 focus:outline-none flex-shrink-0">
      <IoIosNotifications className="h-6 w-6" />
    </button>
  );
}

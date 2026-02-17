import { IoIosNotifications } from "react-icons/io";
export default function NotificationsBell() {
  return (
    <button className="flex bg-white p-1 rounded-full text-gray-500 hover:text-gray-700 focus:outline-none flex-shrink-0">
      <IoIosNotifications className="h-6 w-6" />
    </button>
  );
}

import QuickAction from "./quick-action";
import { IoSend } from "react-icons/io5";
import { FaFileInvoiceDollar } from "react-icons/fa";
import { HiOutlineNewspaper } from "react-icons/hi2";
import { BsQrCodeScan } from "react-icons/bs";
import { FaPlus } from "react-icons/fa";

const actions = [
  {
    name: "Send",
    icon: <IoSend className="h-5 w-5" />,
  },
  { name: "Request", icon: <FaFileInvoiceDollar className="h-5 w-5" /> },
  { name: "Bills", icon: <HiOutlineNewspaper className="h-5 w-5" /> },
  { name: "Scan", icon: <BsQrCodeScan className="h-5 w-5" /> },
  { name: "More", icon: <FaPlus className="h-5 w-5" /> },
];

export default function QuickActions() {
  return (
    <>
      {/**MOBILE VIEW */}
      <div className="md:hidden w-full mt-1">
        <h2 className="text-md font-semibold text-gray-500 dark:text-white mb-2">
          Quick Actions
        </h2>
        <ul className="flex justify-between gap-3">
          {actions.map((action) => (
            <li key={action.name} className="flex items-center cursor-pointer ">
              <QuickAction title={action.name} icon={action.icon} />
            </li>
          ))}
        </ul>
      </div>

      {/**DESKTOP VIEW */}
      <div className="w-full h-full">
        <h2 className="text-xl font-semibold text-gray-500 dark:text-white m-2">
          Quick Actions
        </h2>
        <ul className="grid grid-cols-4 grid-rows-1 h-full gap-5">
          {actions
            .filter((action) => action.name !== "More")
            .map((action) => (
              <li
                key={action.name}
                className="col-span-1 row-span-1 flex items-stretch cursor-pointer "
              >
                <QuickAction
                  title={action.name}
                  icon={action.icon}
                  variant="desktop"
                  className="bg-white dark:bg-gray-800 dark:border border-gray-700 rounded-xl shadow p-3.5 h-full hover:scale-105"
                />
              </li>
            ))}
        </ul>
      </div>
    </>
  );
}

import { FaPiggyBank } from "react-icons/fa";
import { IoHome } from "react-icons/io5";
import { MdWallet } from "react-icons/md";
import { RiBarChart2Fill } from "react-icons/ri";
import { NavLinkProps } from "./bottom-navigation";
import NavigationLink from "./navigation-link";
import Image from "next/image";
import UserCard from "../user-card";
import { ThemeSwitcher } from "../theme-switcher";

const links: NavLinkProps[] = [
  {
    href: "/dashboard",
    title: "Home",
    children: <IoHome className="h-5 w-5" />,
  },
  {
    href: "/budgets",
    title: "Budgets",
    children: <MdWallet className="h-5 w-5" />,
  },
  {
    href: "/savings",
    title: "Savings",
    children: <FaPiggyBank className="h-5 w-5" />,
  },
  {
    href: "/analytics",
    title: "Analytics",
    children: <RiBarChart2Fill className="h-5 w-5" />,
  },
];

export default function Sidebar() {
  return (
    <aside className="hidden md:flex md:flex-col md:w-64 px-5 bg-white border-r border-gray-200 dark:bg-gray-800 dark:border-gray-700">
      <div className="flex items-center gap-3 text-2xl font-bold p-4 ">
        <Image
          height={32}
          width={32}
          src="/favicon.ico"
          alt="PocketFlow Logo"
        />
        <h2>PocketFlow</h2>
      </div>

      <div className="flex flex-col flex-1 justify-between items-start">
        <ul className="h-full w-full">
          {links.map((link, index) => (
            <li key={index}>
              <NavigationLink
                href={link.href}
                title={link.title}
                className="flex items-center text-md space-x-3 px-4 py-2"
              >
                {link.children}
              </NavigationLink>
            </li>
          ))}
        </ul>
        <div className="border-t flex justify-between border-gray-150 dark:border-gray-700 py-5 w-full">
          <UserCard name="Tomas" />
          <ThemeSwitcher />
        </div>
      </div>
    </aside>
  );
}

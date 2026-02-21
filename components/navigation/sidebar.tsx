import NavigationLink from "./navigation-link";
import Image from "next/image";
import UserCard from "../user-card";
import { ThemeSwitcher } from "../theme-switcher";
import { NAV_LINKS } from "@/constants/navigation";

export default function Sidebar() {
  return (
    <aside className="hidden md:flex flex-col w-64 px-5 bg-white border-r border-gray-200 dark:bg-gray-800 dark:border-gray-700">
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
          {NAV_LINKS.map((link, index) => (
            <li key={index}>
              <NavigationLink
                href={link.href}
                label={link.label}
                icon={link.icon}
                variant="sidebar"
              />
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

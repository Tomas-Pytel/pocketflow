"use client";
import NavigationLink from "./navigation-link";
import Image from "next/image";
import UserCard from "../user-card";
import { ThemeSwitcher } from "../theme-switcher";
import { NAV_LINKS } from "@/constants/navigation";

export default function Sidebar() {
  return (
    <aside className="hidden md:flex flex-col w-64 px-5 bg-white border-r border-gray-200 dark:bg-gray-800 dark:border-gray-700 sticky top-0">
      <div className="flex items-center gap-3 text-2xl font-bold p-4 dark:text-white ">
        <Image
          height={32}
          width={32}
          src="/favicon.ico"
          alt="PocketFlow Logo"
        />
        <span>PocketFlow</span>
      </div>

      <nav className="flex flex-col flex-1 justify-between items-start">
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
      </nav>

      <div className="border-t flex flex-row items-center justify-between border-gray-150 dark:border-gray-700 py-2 w-full">
        <div className="flex-1 hover:bg-gray-200 dark:hover:bg-gray-700 py-3 px-2 rounded-lg gap-2">
          <UserCard name="Tomas" />
        </div>
        <div className="bg-gray-100 dark:bg-gray-700 rounded-lg">
          <ThemeSwitcher />
        </div>
      </div>
    </aside>
  );
}

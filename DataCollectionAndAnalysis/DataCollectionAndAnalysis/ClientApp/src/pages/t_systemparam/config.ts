import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "natural_frequency",
        label: "natural_frequency"
    },
    {
        key: "current_spindle_speeds",
        label: "current_spindle_speeds"
    },
    {
        key: "max_spindle_speeds",
        label: "max_spindle_speeds"
    },
    {
        key: "current_feedrates",
        label: "current_feedrates"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];



<template>
    <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
        <wtm-create-form :ref="refName" :status="status" :options="formOptions" ></wtm-create-form>
    </wtm-dialog-box>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";
import formMixin from "@/vue-custom/mixin/form-mixin";
import UploadImg from "@/components/page/UploadImg.vue";


@Component({
    mixins: [formMixin()]
})
export default class Index extends Vue {

    // 表单结构
    get formOptions() {
        const filterMethod = (query, item) => {
            return item.label.indexOf(query) > -1;
        };
        return {
            formProps: {
                "label-width": "100px"
            },
            formItem: {
                "Entity.ID": {
                    isHidden: true
                },
             "Entity.natural_frequency":{
                 label: "natural_frequency",
                 rules: [{ required: true, message: "natural_frequency"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.current_spindle_speeds":{
                 label: "current_spindle_speeds",
                 rules: [{ required: true, message: "current_spindle_speeds"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.max_spindle_speeds":{
                 label: "max_spindle_speeds",
                 rules: [{ required: true, message: "max_spindle_speeds"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.current_feedrates":{
                 label: "current_feedrates",
                 rules: [{ required: true, message: "current_feedrates"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>

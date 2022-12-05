using SNICKERS.Client.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using SNICKERS.Shared;
using SNICKERS.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using SNICKERS.Client.Services;
using System.Text.Json.Serialization;
using SNICKERS.util.enums;
using SNICKERS.Client.Helper.enums;
using SNICKERS.Shared.Errors;
using Newtonsoft.Json;

namespace SNICKERS.Client.Pages.School
{
    public partial class Course : SnickersUI
    {
        private List<CourseDTO>? lstcourse { get; set; }
        private List<SchoolDTO> lstSchool { get; set; }

        [Inject]
        CourseService _CourseService { get; set; }


        public TelerikGrid<CourseDTO>? Grid { get; set; }

        public List<int?> PageSizes => true ? new List<int?> { 15, 25, 50, null } : null;
        private int PageSize = 15;
        private int PageIndex { get; set; } = 2;
        private async Task PageChangedHandler(int currPage)
        {
            PageIndex = currPage;
        }

        private List<CourseDTO>? lstCourses { get; set; }
        public TelerikNotification Notification { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            IsLoading = true;
            await LoadLookupData();
            await LoadCourseData();
            IsLoading = false;


        }
        private async Task LoadLookupData()
        {
            lstSchool = await Http.GetFromJsonAsync<List<SchoolDTO>>("api/School/GetSchools", options);
        }

        private async Task LoadCourseData()
        {
            lstCourses = await Http.GetFromJsonAsync<List<CourseDTO>>("api/Course/GetCourses", options);
        }

        public async Task ReadItems(GridReadEventArgs args)
        {
            IsLoading = true;
            DataEnvelope<CourseDTO> result = await _CourseService.GetCoursesService(args.Request);

            if (args.Request.Groups.Count > 0)
            {
                /***
                NO GROUPING FOR THE TIME BEING
                var data = GroupDataHelpers.DeserializeGroups<WeatherForecast>(result.GroupedData);
                GridData = data.Cast<object>().ToList();
                ***/
            }
            else
            {
                lstcourse = result.CurrentPageData.ToList();
            }

            args.Total = result.TotalItemCount;
            args.Data = result.CurrentPageData.ToList();

            IsLoading = false;

            StateHasChanged();
        }


        private async Task EditItem(GridCommandEventArgs e)
        {
            CourseDTO _CourseDTO = e.Item as CourseDTO;
        }

        private void AddItem(GridCommandEventArgs e)
        {
            CourseDTO _CourseDTO = e.Item as CourseDTO;
        }

        private async Task UpdateItem(GridCommandEventArgs e)
        {
            CourseDTO _CourseDTO = e.Item as CourseDTO;
            HttpResponseMessage response = await Http.PutAsJsonAsync("api/Course", _CourseDTO);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await LoadCourseData();

                HandleNotification("Course Saved",
                5000,
                eNumTelerikThemeColor.success, "save");
                StateHasChanged();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.ExpectationFailed)
            {
                //  I'm using 'ExpectionFailed' to pass back Oracle Errors
                var _ValidationResult = JsonConvert.DeserializeObject<List<OraError>>(response.Content.ReadAsStringAsync().Result);
                foreach (var err in _ValidationResult)
                {
                    HandleNotification($"Database error. {err.OraErrorMsg}",
                    0,
                    eNumTelerikThemeColor.error, "error");
                }
                e.IsCancelled = true;
            }
        }

        private async Task DeleteItem(GridCommandEventArgs e)
        {
            CourseDTO _CourseDTO = e.Item as CourseDTO;
            HttpResponseMessage response = await Http.DeleteAsync($"api/Course/DeleteCourse/{_CourseDTO.CourseNo}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HandleNotification("Course Deleted",
                1000,
                eNumTelerikThemeColor.success, "save");
                StateHasChanged();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.ExpectationFailed)
            {
                //  I'm using 'ExpectionFailed' to pass back Oracle Errors
                var _ValidationResult = JsonConvert.DeserializeObject<List<OraError>>(response.Content.ReadAsStringAsync().Result);
                foreach (var err in _ValidationResult)
                {
                    HandleNotification($"Database error. {err.OraErrorMsg}",
                    0,
                    eNumTelerikThemeColor.error, "error");
                }
                e.IsCancelled = true;
            }
        }

        private async Task CreateItem(GridCommandEventArgs e)
        {
            CourseDTO _CourseDTO = e.Item as CourseDTO;
            string _item = JsonConvert.SerializeObject(_CourseDTO);

            HttpResponseMessage response = await Http.PostAsJsonAsync("api/Course/PostCourse", _item);


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await LoadCourseData();

                HandleNotification("Course Added",
                1000,
                eNumTelerikThemeColor.success, "save");
                StateHasChanged();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.ExpectationFailed)
            {
                //  I'm using 'ExpectionFailed' to pass back Oracle Errors
                var _ValidationResult = JsonConvert.DeserializeObject<List<OraError>>(response.Content.ReadAsStringAsync().Result);
                foreach (var err in _ValidationResult)
                {
                    HandleNotification($"Database error. {err.OraErrorMsg}",
                    0,
                    eNumTelerikThemeColor.error, "error");
                }
                e.IsCancelled = true;
            }
        }


        public void HandleNotification(string strNotificationText,
            int Length,
            eNumTelerikThemeColor eNumTelerikThemeColor, string TelerikIcon = "gear")
        {
            Notification.Show(new NotificationModel()
            {
                Icon = TelerikIcon,
                ShowIcon = true,
                ThemeColor = Utility.GetDescription(eNumTelerikThemeColor),
                Text = strNotificationText,
                CloseAfter = Length,
                Closable = Length == 0 ? true : false
            });
        }

    }
}

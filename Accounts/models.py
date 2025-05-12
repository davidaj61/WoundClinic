from django.db import models
from django.utils.translation import gettext_lazy as _
from django.contrib.auth.models import AbstractUser,PermissionsMixin,AnonymousUser
from .managers import UserManager


class Users(AbstractUser):
    
    national_code = models.CharField(max_length=10, unique=True)
    email=models.EmailField(blank=True,null=True)

    is_active=models.BooleanField(default=True)
    is_admin=models.BooleanField(default=False)
    is_staff=models.BooleanField(default=False)
    
    USERNAME_FIELD='national_code'
    objects=UserManager()
    
    def __str__(self):
        return self.national_code 
    
    
    def has_perm(self, perm: str, obj: models.Model | AnonymousUser | None = ...) -> bool:
        return super().has_perm(perm, obj)
    
    def has_module_perms(self, app_label: str) -> bool:
        return super().has_module_perms(app_label)
    
    @property
    def check_last_login(self):
        if self.last_login:
            return True
        return False
    
    
    def get_full_name(self):
        full_name = "%s %s" % (self.national_code.first_name, self.national_code.last_name)
        return full_name.strip()